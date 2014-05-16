<?php $TEMP_REQUEST = $_REQUEST; ?>
<?php require_once(realpath("../../../wp-load.php"));?>
<?php $_REQUEST = $TEMP_REQUEST; ?>
<?php require_once("logic/core.php"); ?>
<?php require_once("logic/LandingProvider.php"); ?>
<?php
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
?>
<?php get_header(); ?>
<div id="main" class="wrapper">
	<div id="primary" class="site-content">
		<div id="content" role="main">
            <article id="ql-error-page" class="ql-error-page page type-page status-publish hentry">
		        <header class="entry-header">
					<h1 class="entry-title"><?php echo _e("Login Error");?></h1>
		        </header>
		        <div class="entry-content">
                    <p>
                        <?php echo htmlspecialchars($response->error);?>
                    </p>
                    <p>
                        <a href="<?php echo wp_login_url( $response->returnurl );?>"><?php echo _e("Try login again");?></a>&nbsp;&nbsp;&nbsp;
                        <a href="<?php echo site_url();?>"><?php echo _e("Back home");?></a>
                    </p>
				</div><!-- .entry-content -->
	        </article><!-- #post -->
		</div><!-- #content -->
	</div><!-- #primary -->
    <?php get_sidebar(); ?>
</div>
<?php get_footer(); ?>
