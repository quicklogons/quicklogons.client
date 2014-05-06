<?php
class Settings
{
    public $url = "";
    public $secure_url = "";
    public $site_key = "";
    public $site_secret = "";
    public $hash_algorithm = "";

    public static function load()
    {
        $settings = new Settings();
        $raw_settings = parse_ini_file("site.ini", TRUE);
        $ql_settings = $raw_settings["quicklogons"];
        $settings->url = isset($ql_settings["url"]) ? $ql_settings["url"] : "http://www.quicklogons.com";
        $settings->secure_url = isset($ql_settings["secure_url"]) ? $ql_settings["secure_url"] : "https://www.quicklogons.com";
        $settings->site_key = $ql_settings["site_key"];
        $settings->site_secret = $ql_settings["site_secret"];
        $settings->hash_algorithm = isset($ql_settings["hash_algorithm"]) ? $ql_settings["hash_algorithm"] : "HMACSHA1";
        return $settings;
    }
}
?>