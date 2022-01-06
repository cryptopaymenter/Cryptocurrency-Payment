<?
	header('P3P: CP="NOI CURa ADMa DEVa TAIa OUR DELa BUS IND PHY ONL UNI COM NAV INT DEM PRE"');
	header("Pragma: no-cache");
	header("Cache-Control: no-cache,must-revalidate");
?>

<?php

require("config.php");

$reqip = $_SERVER['REMOTE_ADDR'];

function post_url_fsockopen($host, $port, $apiurl, $apiname, $body) //$api_host, 0, $api_url, (body)
{
	if($port == 0 || $port == null || empty($port)) {
		$port = 80;
	}

	$context = stream_context_create([
		'ssl' => [
			'verify_peer' => false,
			'verify_peer_name' => false
		]
	]);
	
	try
	{
		set_error_handler(function(){exit("API server connection error");});
		$fp = stream_socket_client("ssl://".$host.":".$port, $errno, $errstr, 15, STREAM_CLIENT_CONNECT, $context) or exit("API server connection error");
		restore_error_handler();
		if(!fp) {
			exit("fail to connect api server error");
			return false;
		}
		
		$request = '';
		$request .= "POST ".$apiurl."/".$apiname." HTTP/1.1\r\n";
		$request .= "Host: ".$host."\r\n";
		$request .= "Content-Type: application/x-www-form-urlencoded; application/json\r\n";
		$request .= "Content-Length: ".strlen($body)."\r\n";
		$request .= "Connection: close\r\n";
		$request .= "\r\n";
		$request .= $body;
		fwrite($fp, $request);
		
		$response = '';
		while(!feof($fp)){
		 $response .= fread($fp,1024);
		}
		fclose( $fp );
		
		$temp = explode("\r\n\r\n", $response);
		if(count($temp >= 2)) {
			$response = $temp[1];
		}
		
		return $response;
	}
	catch(Exception $e)
	{
		exit("API server connection error");
		return false;
	}
}


function get_apiserver_minimumfee($ccID) {
	global $maxcount_ccID;
	global $reqip;
	global $api_host;
	global $api_url;
	if($ccID >= $maxcount_ccID) {exit("undefined currency id error -> ".$ccID);} //Be sure to filter the ccID to avoid being blocked by the API server
	if(!isset($ccID)) { exit("null currency id error"); }
	if(strlen($reqip) < 7 || strlen($reqip) > 45) { exit("fail to identify ip"); }

	$request_body = "{";
	$request_body .= "\"reqip\":\"$reqip\",";
	$request_body .= "\"ccID\":{$ccID}";
	$request_body .= "}";

	$result = post_url_fsockopen($api_host, 443, $api_url, "getminimumfee", $request_body);
	return $result;
}

function get_apiserver_walletaddress($ccID) {
	global $maxcount_ccID;
	global $reqip;
	global $api_host;
	global $api_url;
	if($ccID >= $maxcount_ccID) { exit("undefined currency id error -> ".$ccID);} //Be sure to filter the ccID to avoid being blocked by the API server
	if(!isset($ccID)) { exit("null currency id error"); }
	if(strlen($reqip) < 7 || strlen($reqip) > 45) { exit("fail to identify ip"); }


	$request_body = "{";
	$request_body .= "\"reqip\":\"$reqip\",";
	$request_body .= "\"ccID\":{$ccID}";
	$request_body .= "}";


	$result = post_url_fsockopen($api_host, 443, $api_url, "getserverwalletaddress", $request_body);
	return $result;
}
?>