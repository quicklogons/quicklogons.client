<?php require_once("core.php");?>
<?php require_once("logic/UserService.php");?>
<?php $user = get_user();?>
<?php if ($user > "") 
      { 
          $user_service = UserService::create();
          $u = $user_service->getUserByLogin($user);
?>
<p>
    Welcome, <a href="/profile.php"><?php echo htmlspecialchars($u->getDisplayName());?></a>!&nbsp;
    <a href="/logout.php?returnurl=<?php echo urlencode(get_return_url());?>">Logout</a>
</p>
<?php } else { ?>
    <a href="/login.php?returnurl=<?php echo urlencode(get_return_url());?>">Login</a>
<?php } ?>