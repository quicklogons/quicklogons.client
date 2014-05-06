<?php require_once("Settings.php"); ?>
<?php
class LandingParameters
{
    public $provider;
    public $user_id;
    public $name;
    public $email;
    public $return_url;
    public $error;
    public $timestamp;
    public $hash;

    function __construct($params)
    {
        $this->provider = $params["provider"];
        $this->user_id = $params["userid"];
        $this->name = $params["name"];
        $this->email = $params["email"];
        $this->timestamp = $params["timestamp"];
        $this->hash = $params["hash"];
        $this->error = $params["error"];
        $this->return_url = $params["returnurl"];
    }
}

class LandingProvider
{
    static $hash_algos = array(
        "hmacsha1" => "sha1",
        "hmacmd5" => "md5",
        "hmacripemd160" => "ripemd160",
        "hmacsha256" => "sha256",
        "hmacsha384" => "sha384",
        "hmacsha512" => "sha512"
    );

    private $settings;
        
    public static function create()
    {
        return new LandingProvider(Settings::load());
    }

    function __construct($_settings)
    {
        $this->settings = $_settings;
    }

    public function landOn()
    {
        $p = new LandingParameters($_REQUEST);
        $hash = $this->calculateHash($p);
        if ($hash != $p->hash) return new LandingParameters(array("error" => "Hash is incorect"));

        $utc_zone = new DateTimeZone("UTC");
        $rd = DateTime::createFromFormat("Y-m-d H:i:sZ", $p->timestamp, $utc_zone);
        $cd = new DateTime("now", $utc_zone);
        if ($cd > $rd->modify("+60 second")) return new LandingParameters(array("error" => "Request expired"));

        if (isset($p->error) && $p->error > "" || isset($p->user_id) && $p->user_id > "") return $p;

        return new LandingParameters(array("error" => "Wrong UserId"));
    }

    function calculateHash($p)
    {
        $hash_algo = LandingProvider::$hash_algos[strtolower($this->settings->hash_algorithm)];
        $content_hash = $this->settings->site_key . $p->user_id . $p->name . $p->email . $p->timestamp . $p->provider . $p->return_url . $p->error . $this->settings->site_secret;
        return base64_encode(hash_hmac($hash_algo, $content_hash, $this->settings->site_secret, TRUE));
    }
}
?>