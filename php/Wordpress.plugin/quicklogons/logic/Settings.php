<?php
class Settings
{
    static $callback_relative_url = "/wp-content/plugins/quicklogons/loggedin.php?provider={provider}&userid={userid}&name={name}&email={email}&timestamp={timestamp}&hash={hash}&error={error}&returnurl={returnurl}";
    static $login_relative_url = "/wp-content/plugins/quicklogons/login.php";

    public $url = "";
    public $secure_url = "";
    public $site_key = "";
    public $site_secret = "";
    public $hash_algorithm = "";

    public static function load()
    {
        $settings = new Settings();
        $settings->url = Settings::get_option_or_default("quicklogons_url", "http://www.quicklogons.com");
        $settings->secure_url = Settings::get_option_or_default("quicklogons_secure_url", "https://www.quicklogons.com");
        $settings->site_key = Settings::get_option_or_default("quicklogons_site_key", "");
        $settings->site_secret = Settings::get_option_or_default("quicklogons_site_secret", "");
        $settings->hash_algorithm = Settings::get_option_or_default("quicklogons_hash_algorithm", "hmacsha1");
        return $settings;
    }

    function get_option_or_default($option_name, $default_value)
    {
        $v = get_option($option_name);
        if($v === FALSE || $v == "" || $v == NULL) return $default_value;
        return $v;
    }
}
?>