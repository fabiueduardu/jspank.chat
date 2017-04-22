<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$username_add = isset($_REQUEST['username_add']) ? $_REQUEST['username_add'] : null;
$username_remove = isset($_REQUEST['username_remove']) ? $_REQUEST['username_remove'] : null;
$result = array ('isvalid' => false, 'message' => AppService::getMessage('error_404' , $language));

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::getMessage('error_401' , $language));
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get($username);
    
    if(!empty($db_user))
    {
        $UserService = new UserService($dbid);        
        $result = array ('dbid'=> $dbid,'isvalid' => true, 'message' => AppService::getMessage('success' , $language));
        
        if($username_add != null){
            $PostService = new PostService($dbid);
            $db_maxpostid = $PostService -> getmaxpostid()[0]['postid'];                        
            $db_user_tmp = $UserService -> get($username_add , null);
 
            if(empty($db_user_tmp)){
                $UserService -> add($username_add , $db_maxpostid ,$username);
            }else if(!empty($db_user_tmp) && $db_user_tmp[0]['active'] != true){
                    $UserService -> update_active($username_add, true);
                    $result['message'] = AppService::getMessage('success_user_reactivated' , $language);
            }
            else{
               $result['isvalid'] =false;
               $result['message'] =  AppService::getMessage('user_already_exists' , $language);
            }
            
        }
        if($username_remove != null){            
            $db_user_tmp = $UserService -> get($username_remove , null);

            if(empty($db_user_tmp) || $db_user_tmp[0]['active'] != true){
                $result['isvalid'] =false;
                $result['message'] =  AppService::getMessage('error_404' , $language);
            }else if($db_user_tmp[0]['active'] == true){
                    $UserService -> update_active($username_remove, false);
                    $result['message'] =  AppService::getMessage('success_user_inactivated' , $language);                    
            }
           $UserService -> update_active($username_remove, false); 
        }
    }
}

echo json_encode($result);

?>