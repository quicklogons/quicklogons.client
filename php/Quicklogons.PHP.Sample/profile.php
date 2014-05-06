<?php require_once("shared/core.php");?>
<?php require_once("logic/UserService.php");?>
<?php require_authentication();?>
<?php if(section("content")): ?>
<?php 
      $user_service = UserService::create();
      $u = $user_service->getUserByLogin(get_user());
?>
<h2>Profile</h2>
<div class="form-horizontal panel panel-default">
    <div class="panel-body">
        <div class="form-group">
            <label class="col-sm-1 control-label">Login</label>
            <div class="col-sm-10">
                <p class="form-control-static"><?php echo htmlspecialchars($u->login)?></p>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-1 control-label">Name</label>
            <div class="col-sm-10">
                <p class="form-control-static"><?php echo htmlspecialchars($u->name)?></p>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-1 control-label">Email</label>
            <div class="col-sm-10">
                <p class="form-control-static"><?php echo htmlspecialchars($u->email)?></p>
            </div>
        </div>
    </div>
</div>
<?php endif ?>
<?php render_layout("shared/master.php", "Profile"); ?>