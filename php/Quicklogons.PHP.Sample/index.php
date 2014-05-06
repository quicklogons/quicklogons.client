<?php require_once("shared/core.php"); ?>
<?php require_once("logic/LoginProvider.php"); ?>
<?php $login_provider = LoginProvider::create(); ?>
<?php if(section("content")): ?>
<div class="row">
    <p><br /></p>
    <div class="col-sm-4 text-center">
        <a class="btn btn-lg btn-primary" href="<?php echo $login_provider->getLoginPageUrl(get_return_url(NULL));?>">Quicklogons Login page</a><br /><br />
    </div>
    <div class="col-sm-4 text-center">
        <a class="btn btn-lg btn-danger" href="/login.php">Custom Login page</a><br /><br />
    </div>
    <div class="col-sm-4 text-center">
        <a class="btn btn-lg btn-info" href="/profile.php">Profile</a><br /><br />
    </div>
    <p><br /><br /><br /><br /><br /></p>
    <pre>
    <?php var_dump($_SERVER);?>
    <?php var_dump($_REQUEST);?>
    </pre>
</div>
<?php endif ?>
<?php render_layout("shared/master.php", "Home"); ?>