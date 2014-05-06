<?php require_once("shared/core.php");?>
<?php require_once("logic/LoginProvider.php");?>
<?php 
      $login_provider = LoginProvider::create();
      $links = $login_provider->getLoginLinks($_REQUEST["returnurl"]);
?>
<?php if(section("content")): ?>
<h2 class="page-header">Custom Login</h2>
<div class="row">
<?php foreach($links as $link):?>
        <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6 text-center">
            <a class="btn btn-default" href="<?php echo $link->Url?>"><?php echo $link->ProviderName?></a>
            <br /><br />
        </div>
<?php endforeach;?>
</div>
<?php endif ?>
<?php render_layout("shared/master.php", "Custom Login"); ?>