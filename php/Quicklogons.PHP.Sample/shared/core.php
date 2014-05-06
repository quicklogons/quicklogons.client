<?php require_once("properties.php");?>
<?php

$title = NULL;
$currentSection = NULL;
$sections = array();

function title()
{
    global $title;
    if(isset($title) && $title != "") echo htmlspecialchars(" / " . $title);
}

function render_layout($master, $page_title)
{
    global $title;
    $title = $page_title;
    require_once($master);
}

function section($section)
{
    global $currentSection, $sections;
    if ($currentSection == $section)
    {
        return $sections[$section] = TRUE;
    } 
    else if (!section_exists($section)) 
    {
        return $sections[$section] = FALSE;
    }
}

function section_exists($section)
{
    global $sections;
    return isset($sections[$section]);
}

function render_section($section)
{
    if (section_exists($section))
    {
        global $currentSection;
        $currentSection = $section;
        require($_SERVER["PHP_SELF"]);
    }
}

function calculate_layout($left_aside, $content, $right_aside)
{
    $s = array();
    if (!section_exists($left_aside)) $left_aside = NULL;
    if (!section_exists($right_aside)) $right_aside = NULL;
    if (!isset($left_aside) && !isset($right_aside))
    {
        $s[1] = "col-md-12";
    }
    else if (!isset($left_aside) && isset($right_aside))
    {
        $s[1] = "col-md-9";
        $s[2] = "col-md-3";
    }
    else if (isset($left_aside) && !isset($right_aside))
    {
        $s[0] = "col-md-3";
        $s[1] = "col-md-9";
    }
    else
    {
        $s[0] = "col-md-2";
        $s[1] = "col-md-8";
        $s[2] = "col-md-2";
    }
    return $s;
}

function require_authentication()
{
    if (get_user() == "")
    {
        redirect_local("/login.php?returnurl=" . urlencode($_SERVER["URL"]));
    }
}

function get_user()
{
    $user = $_COOKIE["user"];
    if(!isset($user)) $user = "";
    return $user;
}

function login_user($user_name)
{
    $host = $_SERVER["SERVER_NAME"];    
    $path = "/";
    if (strcasecmp($host, "localhost") || strcasecmp($host, "127.0.0.1"))
    {
        $host = "";
        $path = "";
    }
    setcookie("user", $user_name, time() + 3600, $path, $host);
}

function logout_user()
{
    $host = $_SERVER["SERVER_NAME"];    
    $path = "/";
    if (strcasecmp($host, "localhost") || strcasecmp($host, "127.0.0.1"))
    {
        $host = "";
        $path = "";
    }
    setcookie("user", "", time() - 3600, $path, $host);
}

function get_return_url($default = "/")
{
    $return_url = $_SERVER["URL"];
    if ($return_url == "/login.php" || $return_url == "/logout.php" || $return_url == "/loggedin.php") $return_url = $default;
    return $return_url;
}

function validate_url($url)
{
    $host = $_SERVER["HTTP_HOST"];
    if (stripos($url, "http://" . $host) === 0 || stripos($url, "https://" . $host) === 0)
    {
        return $url;
    }
    elseif (stripos($url, "/") === 0)
    {
        return "http://" . $host . $url;
    }
    return "http://" . $host;
}

function redirect_local($url)
{
    header('Location: ' . validate_url($url), TRUE, 302);
    die();
}
?>