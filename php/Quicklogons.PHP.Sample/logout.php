<?php require_once("shared/core.php");?>
<?php
    logout_user();
    redirect_local($_REQUEST["returnurl"]);
?>