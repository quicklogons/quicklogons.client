<?php
class User
{
    public $login;
    public $name;
    public $email;

    function __construct($user_or_login)
    {

        if ($user_or_login instanceof SimpleXMLElement)
        {
            $this->login = (string)$user_or_login->login;
            $this->name = (string)$user_or_login->name;
            $this->email = (string)$user_or_login->email;
        }
        else
        {
            $this->login = (string)$user_or_login;
        }
    }

    function getDisplayName()
    {
        if(isset($this->name) && $this->name > "") return $this->name;
        elseif (isset($u->email) && $u->email > "") return $this->email;
        return $this->login;
    }
}

class UserService
{
    private $db_path = "";

    public static function create()
    {
        return new UserService();
    }

    function __construct()
    {
        $this->db_path = realpath("db/users.xml");
    }

    public function getUserByLogin($login)
    {
        $xd = simplexml_load_file($this->db_path);
        foreach($xd->children() as $xu)
        {
            if (((string)$xu->login) == $login) return new User($xu);
        }
        return NULL;
    }

    public function saveUser($user)
    {
        $xd = simplexml_load_file($this->db_path);
        foreach($xd->children() as $xu)
        {
            if (((string)$user->login) == ((string)$xu->login)) break;
            unset($xu);
        }
        if (!isset($xu))
        {
            $xu = $xd->addChild("user");
            $xu->login = $user->login;
        }
        $xu->name = $user->name;
        $xu->email = $user->email;
        $xd->asXML($this->db_path);
    }
}
?>