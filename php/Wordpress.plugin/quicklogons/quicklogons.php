<?php
/*
Plugin Name: Quicklogons
Plugin URI: http://wordpress.org/plugins/quicklogons/
Description: Fast and easy get users logged in on your web-site with bunch of OAuth providers like Google, Facebook, Twitter and more.
Author: Maxim Postevoy
Version: 1.0
Author URI: http://www.quicklogons.com/
*/
?>
<?php
if(!class_exists("QuicklogonsPlugin"))
{
    require_once(sprintf("%s/logic/LoginProvider.php", dirname(__FILE__)));

    class QuicklogonsPlugin
    {
        public $default_login_url;
        private $login_provider;

        public function __construct()
        {
            $this->login_provider = LoginProvider::create();

            $this->default_login_url = wp_login_url();

            add_action('admin_init', array(&$this, 'admin_init'));
            add_action('admin_menu', array(&$this, 'add_menu'));
            add_filter('login_url', array(&$this, 'ql_login_url'));
        }

        public static function activate()
        {
        }

        public static function deactivate()
        {
        }

        public function admin_init()
        {
            $this->init_settings();
        }

        function init_settings()
        {
            register_setting('quicklogons_plugin-group', 'quicklogons_url');
            register_setting('quicklogons_plugin-group', 'quicklogons_secure_url');
            register_setting('quicklogons_plugin-group', 'quicklogons_site_key');
            register_setting('quicklogons_plugin-group', 'quicklogons_site_secret');
            register_setting('quicklogons_plugin-group', 'quicklogons_hash_algorithm');
        }

        public function ql_login_url($login_url, $return_url)
        {
            //return $this->login_provider->getLoginPageUrl($return_url);
            return site_url(Settings::$login_relative_url) . ($return_url ? ("?returnurl=" . urlencode($return_url)) : "");
        }

        public function add_menu()
        {
            add_options_page('Settings', 'Quicklogons', 'manage_options', 'quicklogons_plugin', array(&$this, 'plugin_settings_page'));
        }

        public function plugin_settings_page()
        {
            if(!current_user_can('manage_options'))
            {
                wp_die(__('You do not have sufficient permissions to access this page.'));
            }
            
            include_once(sprintf("%s/logic/Settings.php", dirname(__FILE__)));
            include_once(sprintf("%s/logic/LandingProvider.php", dirname(__FILE__)));
            include_once(sprintf("%s/templates/settings.php", dirname(__FILE__)));
        }
    }
}
?>
<?php
if(class_exists("QuicklogonsPlugin"))
{
    register_activation_hook(__FILE__, array('QuicklogonsPlugin', 'activate'));
    register_deactivation_hook(__FILE__, array('QuicklogonsPlugin', 'deactivate'));

    $quicklogons_plugin = new QuicklogonsPlugin();
}
?>