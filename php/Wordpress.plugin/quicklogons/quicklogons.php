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
    require_once(sprintf("%s/logic/core.php", dirname(__FILE__)));
    require_once(sprintf("%s/logic/LoginProvider.php", dirname(__FILE__)));
    require_once(sprintf("%s/logic/LandingProvider.php", dirname(__FILE__)));

    class QuicklogonsPlugin
    {
        public $default_login_url;

        public function __construct()
        {
            $this->default_login_url = wp_login_url();

            add_action('init', array(&$this, 'init'));
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

        public function init()
        {
            if ($_SERVER["PHP_SELF"] != "/wp-login.php") return;
            if ($this->is_login_request()) $this->log_in();
            if ($this->is_landing_request()) $this->land_on();
        }
                
        function is_landing_request()
        {
            $keys = array_change_key_case($_REQUEST);
            return array_key_exists("provider", $keys) &&
                   array_key_exists("userid", $keys) &&
                   array_key_exists("name", $keys) &&
                   array_key_exists("email", $keys) &&
                   array_key_exists("timestamp", $keys) &&
                   array_key_exists("hash", $keys) &&
                   array_key_exists("error", $keys) &&
                   array_key_exists("returnurl", $keys);
        }

        function is_login_request()
        {
            $keys = array_change_key_case($_REQUEST);
            return array_key_exists("oauth", $keys) || 
                   array_key_exists("loggedout", $keys);
        }

        function log_in()
        {
            global $quicklogons_plugin;
            $login_provider = LoginProvider::create();
            $links = $login_provider->getLoginLinks($_REQUEST["returnurl"]);
            require_once(sprintf("%s/templates/login.php", dirname(__FILE__)));
            die;
        }

        function land_on()
        {
            $landing_provider = LandingProvider::create();
            $response = $landing_provider->landOn();
            if (!isset($response->error) || $response->error == "")
            {
                $login = $response->user_id . "@" . $response->provider;
                $user = get_user_by("login", $login);
                $email = $response->email > "" && !email_exists($response->email) ? $response->email : FALSE;
                if (!$user)
                {
                    $user_id = wp_create_user($login, wp_generate_password(), $email ? $email : NULL);
                    if ($user_id) $user = get_user_by("id", $user_id);
                    else $response->error = _e("Wordpress can't create user");
                }
                if ($user)
                {
                    $user_data = array('ID' => $user->data->ID);
                    if ($email) $user_data["user_email"] = $email;
                    $user_data["display_name"] = get_display_name($response);
                    wp_update_user($user_data);
                    wp_set_current_user( $user->data->ID, $user->data->user_login );
                    wp_set_auth_cookie( $user->data->ID );
                    do_action( 'wp_login', $user->data->user_login );
                    redirect_local($response->return_url);
                }
            }
            require_once(sprintf("%s/templates/loginfail.php", dirname(__FILE__)));
            die;
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
            
            $settings = Settings::load();
            $callback_template = site_url(Settings::$callback_relative_url);
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