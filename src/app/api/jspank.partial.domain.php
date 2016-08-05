<?php

/*### Repositories*/
class Repository {
    
    protected $db;
    
    function __construct($db) {
        $this -> db = $db;
    }
    
    protected function _prepare($prepare , $params = null){
        if($params != null)
        foreach ($params as $key => &$val)
        $prepare -> bindParam($key, $val);
    }
    
    protected function executeNonQuery($query , $params = null){
        $prepare = $this -> db -> prepare($query);
        $this-> _prepare($prepare ,  $params);
        return $prepare -> execute() == 1;
    }
    
    protected function executeReader($query , $params = null){
        $prepare =  $this -> db -> prepare($query);
        $this-> _prepare($prepare ,  $params);
        $prepare->execute();
        return $prepare->fetchAll(PDO::FETCH_ASSOC);
    }
}

class AppRepository extends  Repository {
    
    private $queries = array(
    'setting_create' => 'CREATE TABLE IF NOT EXISTS setting (settingid INTEGER PRIMARY KEY NOT NULL,name VARCHAR(255) NOT NULL, description VARCHAR(255) NOT NULL, datecreate DATETIME NOT NULL);'
    ,'user_create' => 'CREATE TABLE IF NOT EXISTS user (username VARCHAR(100) PRIMARY KEY NOT NULL,active BOOLEAN NOT NULL DEFAULT 1, datecreate DATETIME NOT NULL);'
    ,'post_create' =>'CREATE TABLE IF NOT EXISTS post (postid INTEGER PRIMARY KEY AUTOINCREMENT , post VARCHAR(8000) NOT NULL, datecreate DATETIME NOT NULL, username VARCHAR(100) NOT NULL, FOREIGN KEY(username) REFERENCES user(username));'
    );
    
    function __construct($db){
        parent::__construct($db);
    }
    
    public function init(){
        return   $this -> executeNonQuery($this -> queries["setting_create"])
        && $this -> executeNonQuery($this -> queries["user_create"])
        && $this -> executeNonQuery($this -> queries["post_create"]);
    }
}

class SettingRepository extends  Repository {
    
    private $queries = array(
    'setting_insert' => 'INSERT INTO setting(settingid,name,description,datecreate)  values(:settingid,:name,:description,julianday(\'now\'));'
    );
    
    function __construct($db){
        parent::__construct($db);
    }
    
    public function add($settingid, $name, $description){
        return $this -> executeNonQuery($this -> queries["setting_insert"] ,  array(':settingid' => $settingid,':name' => $name,':description' => $description));
    }
}

class UserRepository extends  Repository {
    
    private $queries = array(
    'user_insert'=> 'INSERT INTO user(username,datecreate)  values(:username,julianday(\'now\'));'
    ,'user_update_active'=> 'UPDATE user  SET active = :active WHERE username = :username'
    ,'user_select_target'=> 'SELECT * FROM user WHERE username = :username and (:active is null or active = :active);'
    );
    
    function __construct($db){
        parent::__construct($db);
    }
    
    public function add($username){
        return $this -> executeNonQuery($this -> queries["user_insert"] ,  array(':username' => $username));
    }
    
    public function update_active($username,$active){
        return $this -> executeNonQuery($this -> queries["user_update_active"] ,  array(':username' => $username, ':active' => $active));
    }
    
    public function get($username , $active = true){
        return $this -> executeReader($this -> queries["user_select_target"] ,  array(':username' => $username, ':active' => $active));
    }
}

class PostRepository extends  Repository {
    
    private $queries = array(
    'post_insert'=> 'INSERT INTO post(post,username,datecreate)  values(:post,:username,julianday(\'now\'));'
    ,'post_select_foward' => 'SELECT * FROM post WHERE postid > :postid order by postid;'
    ,'post_select_back' => 'SELECT * FROM post WHERE postid < :postid order by postid;'
    );
    
    function __construct($db){
        parent::__construct($db);
    }
    
    public function add($username, $post){
        return $this -> executeNonQuery($this -> queries["post_insert"] ,  array(':post' => $post,':username' => $username));
    }
    
    public function get($postid , $foward = true){
        return $this -> executeReader($this -> queries[($foward ? "post_select_foward" : 'post_select_back') ] ,  array(':postid' => $postid ));
    }
}

/*### Services*/
class Service {
    
    protected $db;
    
    function __construct($dbname) {
        $this -> db = new PDO('sqlite:data/'.$dbname);
    }
    
}

class AppService extends Service {
    
    protected $repository;

    const message = array(
            'success' => 'success'
            ,'error' => 'there was an error, please try again later'
            ,'error_401' => 'error, 401 unauthorized'
            ,'error_404' => 'error, 404 not found'
            ,'error_500' => 'error, 500 internal server error'
        );

    function __construct($dbname){
        parent::__construct($dbname);
        $this -> repository = new AppRepository($this -> db);
    }
    
    public function init(){
        return $this -> repository -> init();
    }
    
    public static function has($dbname){
        return file_exists('data/'.$dbname);
    }

    public static function newguid()
    {
        mt_srand((double)microtime()*10000);
        $charid = strtoupper(md5(uniqid(rand(), true)));
        $hyphen = chr(45);
        $uuid = substr($charid, 0, 8).$hyphen
        .substr($charid, 8, 4).$hyphen
        .substr($charid,12, 4).$hyphen
        .substr($charid,16, 4).$hyphen
        .substr($charid,20,12);
        return $uuid;
    }
        
    public static function errorHandler($errNo, $errStr, $errFile, $errLine) {
        $result = array ('isvalid'=> false, 'message' => "$errStr in $errFile on line $errLine");
        echo json_encode($result);
        die();
    }
    
}

class SettingService extends Service {
    
    protected $repository;
    
    function __construct($dbname){
        parent::__construct($dbname);
        $this -> repository = new SettingRepository($this -> db);
    }
    
    public function add($settingid, $name, $description){
        return $this -> repository -> add($settingid, $name, $description);
    }
}

class UserService extends Service {
    
    protected $repository;
    
    function __construct($dbname){
        parent::__construct($dbname);
        $this -> repository = new UserRepository($this -> db);
    }
    
    public function add($username){
        return $this -> repository -> add($username);
    }
    
    public function update_active($username,$active){
        return $this -> repository -> update_active($username, $active);
    }
    
    public function get($username , $active = true){
        return $this -> repository -> get($username, $active);
    }
}

class PostService extends Service {
    
    protected $repository;
    
    function __construct($dbname){
        parent::__construct($dbname);
        $this -> repository = new PostRepository($this -> db);
    }
    
    public function add($username,$post){
        return $this -> repository -> add($username,$post);
    }
    
    public function get($postid , $foward = true){
        return $this -> repository -> get($postid,$foward);
    }
}
?>