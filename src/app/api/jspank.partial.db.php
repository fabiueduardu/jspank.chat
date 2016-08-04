<?php
$take = 100;
$queries = array(
     'setting_create' => 'CREATE TABLE IF NOT EXISTS setting (settingid INTEGER PRIMARY KEY NOT NULL,name VARCHAR(255) NOT NULL, description VARCHAR(255) NOT NULL, datecreate DATETIME NOT NULL);'
    ,'setting_insert' => 'INSERT INTO setting(settingid,name,description,datecreate)  values(:settingid,:name,:description,julianday(\'now\'));'

    ,'user_create' => 'CREATE TABLE IF NOT EXISTS user (username VARCHAR(100) PRIMARY KEY NOT NULL,active BOOLEAN NOT NULL DEFAULT 1, datecreate DATETIME NOT NULL);'
    ,'user_insert'=> 'INSERT INTO user(username,datecreate)  values(:username,julianday(\'now\'));'
    ,'user_update_active'=> 'UPDATE user  SET active = :active WHERE username = :username'
    ,'user_select_target'=> 'SELECT * FROM user WHERE username = :username and (:active is null or active = :active);'
   
    ,'post_create' =>'CREATE TABLE IF NOT EXISTS post (postid INTEGER PRIMARY KEY AUTOINCREMENT , post VARCHAR(8000) NOT NULL, datecreate DATETIME NOT NULL, username VARCHAR(100) NOT NULL, FOREIGN KEY(username) REFERENCES user(username));'
    ,'post_insert'=> 'INSERT INTO post(post,username,datecreate)  values(:post,:username,julianday(\'now\'));'
    ,'post_select_foward' => 'SELECT * FROM post WHERE postid > :postid order by postid;'
    ,'post_select_back' => 'SELECT * FROM post WHERE postid < :postid order by postid;'
    
);

function DbInstance($dbname){
   return new PDO('sqlite:data/'.$dbname);
}

function DbExecuteNonQuery($db , $query , $params = null){
    $prepare = $db -> prepare($query);
    DbPrepare($prepare ,  $params);
    return $prepare->execute() == 1;
}

function DbExecuteRead($db , $query , $params = null){
    $prepare = $db -> prepare($query);
      DbPrepare($prepare ,  $params);
    $prepare->execute();
    return $prepare->fetchAll(PDO::FETCH_ASSOC);
}

function DbPrepare($prepare , $params = null){

  if($params != null)
        foreach ($params as $key => &$val)
            $prepare -> bindParam($key, $val);
}   

?>