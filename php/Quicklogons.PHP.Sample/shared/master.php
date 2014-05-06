<?php global $version;?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>QuickLogons Client / PHP Sample<?php title();?></title>

    <link href="/content/bootstrap-3.1.0/css/bootstrap.min.css?version=<?php echo $version;?>" rel="stylesheet">
    <link href="/content/styles.css?version=<?php echo $version;?>" rel="stylesheet">
    <!--[if lt IE 9]>
      <script src="/content/bootstrap-3.1.0/js/html5shiv.js?version=<?php echo $version;?>"></script>
      <script src="/content/bootstrap-3.1.0/js/respond.min.js?version=<?php echo $version;?>"></script>
    <![endif]-->
</head>
<body>
    <main class="container">
        <header>
<?php require_once("user.php");?>
            <div class="">
                <a class="" href="/"><h1>QuickLogons Client - PHP Sample</h1></a>
            </div>
        </header>

<?php if(section_exists("before-main")) { ?>
            <div class="row">
                <section class="col-md-12 before-main">
                    <?php render_section("before-main");?>
                </section>
            </div>
<?php } ?>

<?php $s = calculate_layout("LeftAside", "Content", "RightAside"); ?>
        <section class="row">
    <?php if (isset($s[0])) { ?>
                <aside class="<?php echo $s[0];?> aside-left">
                    <?php render_section("left-aside");?>
                </aside>
    <?php } ?>
            <div class="<?php echo $s[1];?> wrap-content">
    <?php if(section_exists("before-content")) { ?>
                    <section class="before-content">
                        <?php render_section("before-content");?>
                    </section>
    <?php } ?>
                <article class="content">
                    <?php render_section("content");?>
                </article>
    <?php if(section_exists("after-content")) { ?>
                    <section class="after-content">
                        <?php render_section("after-content");?>
                    </section>
    <?php } ?>
            </div>
    <?php if (isset($s[2])) { ?>
                <aside class="<?php echo $s[2];?> aside-right">
                    <?php render_section("right-aside");?>
                </aside>
    <?php } ?>
        </section>

<?php if(section_exists("after-main")) { ?>
            <section class="after-main">
                <?php render_section("after-main");?>
            </section>
<?php } ?>
    </main>
    <footer class="container">
        <div>
            <div class="text-muted pull-left">
                © QuickLogons 2014&nbsp;
            </div>
            <div class="text-muted pull-right">Powered by <a target="_blank" href="http://www.php.net/">PHP</a> &amp; <a target="_blank" href="http://getbootstrap.com/">Bootstrap</a></div>
        </div>
    </footer>
    <script src="/content/jquery/jquery-2.0.3.min.js?version=<?php echo $version;?>"></script>
    <script src="/content/bootstrap-3.1.0/js/bootstrap.min.js?version=<?php echo $version;?>"></script>
    <script src="/content/scripts.js?version=<?php echo $version;?>"></script>
<?php if(section_exists("scripts")) render_section("scripts");?>
</body>
</html>