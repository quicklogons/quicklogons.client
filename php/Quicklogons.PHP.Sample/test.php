<?php require_once("logic/UserService.php"); ?>
<?php
    $user_service = new UserService();
    $user_login = "Maxim2";
    $user = $user_service->getUserByLogin($user_login);
    if (!isset($user)) $user->login = $user_login;
    $user->name = "Test";
    $user->email = "";
    $user_service->saveUser($user);
    var_dump($user);
?>