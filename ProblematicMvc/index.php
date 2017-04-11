<?
    $fileread = fopen("~/App_Data/Big.txt", "r") or die("Unable to open file!");
	
    echo fread($myfile,filesize("Big.txt"));
?>