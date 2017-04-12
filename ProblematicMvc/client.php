<?php
//$connstr = getenv("MYSQLCONNSTR_MySqlDB");
//echo "MySQL Connection String is".$connstr;

foreach ($_SERVER as $key => $value)
{
    if (strpos($key, "MYSQLCONNSTR_") !== 0)
    {
        continue;
    }

    $hostname = preg_replace("/^.*Data Source=(.+?);.*$/", "\\1", $value);
    $username = preg_replace("/^.*User Id=(.+?);.*$/", "\\1", $value);
    $password = preg_replace("/^.*Password=(.+?)$/", "\\1", $value);
    break;
}
/* now you can use the $host, $username, $password like you normally would */
//connection to the database
$dbhandle = mysql_connect($hostname, $username, $password) or die("Unable to connect to MySQL");
echo "Connected to MySQL<br>";

sleep(10);

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