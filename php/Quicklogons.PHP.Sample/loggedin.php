<?php require_once("shared/core.php"); ?>
<?php require_once("logic/LandingProvider.php");?>
<?php require_once("logic/UserService.php");?>
<?php
    $user_service = UserService::create();
    $landing_provider = LandingProvider::create();
    $response = $landing_provider->landOn();
    if (!isset($response->error) || $response->error == "")
    {
        $login = $response->user_id . "@" . $response->provider;
        $user = $user_service->getUserByLogin($login);
        if (is_null($user)) $user = new User($login);
        $user->name = $response->name;
        $user->email = $response->email;
        $user_service->saveUser($user);
        login_user($login);
        redirect_local($response->return_url);
    }
?>
<?php if(section("content")): ?>
<h2>Login Error</h2>
<p>
    <?php echo htmlspecialchars($response->error);?>
</p>
<?php endif ?>
<?php render_layout("shared/master.php", "Login Error"); ?>