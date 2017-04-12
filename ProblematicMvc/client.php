<?php

    $username = "b5ac1075ed491b";
    $password = "408a5e91";
    $hostname = "ap-cdbr-azure-southeast-b.cloudapp.net";

    //connection to the database
    $dbhandle = mysql_connect($hostname, $username, $password) or die("Unable to connect to MySQL");
    echo "Connected to MySQL<br>";

    sleep(100);

    //select a database to work with
    $selected = mysql_select_db("kauwpdb",$dbhandle) or die("Could not select kauwpdb");

    //execute the SQL query and return records
    $result = mysql_query("SELECT * FROM *") ;//or die("Could not query database");

    echo $result;
    //fetch tha data from the database
    //while ($row = mysql_fetch_array($result)) {
    //  echo "ID:".$row{'id'}." Name:".$row{'model'}."Year: ". //display the results
    //$row{'year'}."<br>";
    //}
    //close the connection
    //mysql_close($dbhandle);
?>