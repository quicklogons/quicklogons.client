<?php

function get_display_name($response)
{
    if(isset($response->name) && $response->name > "") return $response->name;
    elseif (isset($response->email) && $response->email > "") return $response->email;
    return $response->user_id . "@" . $response->provider;
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