<?php require_once(realpath("../../../wp-load.php"));?>
<?php require_once("logic/core.php"); ?>
<?php require_once("logic/LoginProvider.php"); ?>
<?php 
      $login_provider = LoginProvider::create();
      $links = $login_provider->getLoginLinks($_REQUEST["returnurl"]);
?>
<?php get_header(); ?>
<div id="main" class="wrapper">
	<div id="primary" class="site-content">
		<div id="content" role="main">
            <article id="ql-error-page" class="ql-error-page page type-page status-publish hentry">
		        <header class="entry-header">
					<h1 class="entry-title"><?php echo _e("Login with");?></h1>
		        </header>
		        <div class="entry-content">
                <?php foreach($links as $link):?>
                    <a style="font-size: large;" href="<?php echo $link->Url?>"><?php echo $link->ProviderName?></a>&nbsp;&nbsp;
                <?php endforeach;?>
				</div><!-- .entry-content -->
                <br><br>
		        <header class="entry-header">
				    <h1 class="entry-title"><?php echo _e("... or just");?></h1>
		        </header>
		        <div class="entry-content">
                    <a style="font-size: large;" href="<?php echo $quicklogons_plugin->default_login_url;?>"><?php echo _e("Log In");?></a>
				</div><!-- .entry-content -->
	        </article><!-- #post -->
		</div><!-- #content -->
	</div><!-- #primary -->
    <?php get_sidebar(); ?>
</div>
<?php get_footer(); ?>