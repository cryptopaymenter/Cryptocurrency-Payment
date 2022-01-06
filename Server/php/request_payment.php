
<?
	header('P3P: CP="NOI CURa ADMa DEVa TAIa OUR DELa BUS IND PHY ONL UNI COM NAV INT DEM PRE"');
	header("Pragma: no-cache");
	header("Cache-Control: no-cache,must-revalidate");
?>
<?php
//Sending non-POST requests may be blocked by the API server!
require("config.php");
require("util.php");

global $reqip;
$ccID = $_POST['ccID'];
$amount = $_POST['amount'] * 1000000;
$paymentPS = $_POST['payment_password'];
$billSender = $_POST['sender']; //end user wallet address

//Be sure to filter below if() contents
if($ccID >= $maxcount_ccID) { exit("undefined currency id error"); } //Be sure to filter the ccID to avoid being blocked by the API server
if(!isset($ccID)) { exit("null currency id error"); }
if(!isset($amount) || $amount == 0) { exit("amount error"); }
if(strlen($reqip) < 7 || strlen($reqip) > 45) { exit("fail to identify ip error"); }
if (strlen($paymentPS) > 0 && !ctype_alnum($paymentPS)) { exit("Special characters cannot be used in the payment password. (error)"); }
if (strlen($paymentPS) > 16) { exit("Too long passworde error"); }
if (strlen($billSender) < $min_wallet_addr_length || strlen($billSender) > $max_wallet_addr_length || !ctype_alnum($billSender)) { exit("wallet address error"); }

$request_body = "{";
$request_body .= "\"reqip\":\"$reqip\",";
$request_body .= "\"ccID\":{$ccID},";
$request_body .= "\"amount\":{$amount},";
$request_body .= "\"paymentPS\":\"{$paymentPS}\",";
$request_body .= "\"billSender\":\"{$billSender}\"";
$request_body .= "}";

$result = post_url_fsockopen($api_host, 443, $api_url, "requestpayment", $request_body);

if (strpos(strtolower($result), "success") === false) { exit("fail to payment error<br>".$result); }

$apiserver_wallet_json_result = get_apiserver_walletaddress($ccID);
if (strpos(strtolower($apiserver_wallet_json_result), "success") === false) { exit("fail to get api server wallet error<br>".$apiserver_wallet_json_result); }

$apiserver_wallet = json_decode($apiserver_wallet_json_result);
$apiserver_wallet = $apiserver_wallet->ServerWalletAddress;
echo "success $apiserver_wallet"; //Print the wallet address of the API server
?>
