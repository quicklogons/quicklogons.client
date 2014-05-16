<?php require_once("Settings.php"); ?>
<?php
class LoginProvider
{
    static $login_path = "/login/site/";

    private $settings;
    private $login_page_url;
        
    public static function create()
    {
        return new LoginProvider(Settings::load());
    }

    function __construct($_settings)
    {
        $this->settings = $_settings;
        $this->login_page_url = $this->settings->secure_url . LoginProvider::$login_path . $this->settings->site_key;
    }

    public function getLoginPageUrl($returnurl)
    {
        return isset($returnurl) && $returnurl > "" ? $this->login_page_url . "?returnurl=" . urlencode($returnurl) : $this->login_page_url;
    }

    public function getLoginLinks($returnurl)
    {
        $url = $this->settings->secure_url . LoginProvider::$login_path . $this->settings->site_key . ".json";
        $url = isset($returnurl) ? $url . "?returnurl=" . urldecode($returnurl) : $url;
        $json = file_get_contents($url);
        if ($json == FALSE) return FALSE;
        return json_decode($json);
    }
}
?>