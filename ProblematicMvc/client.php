<html>
    <body>
<?php
    $connstr = getenv("MYSQLCONNSTR_MySqlDB");    
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
    echo "Server Name: ".$hostname."</br>";
    /* now you can use the $host, $username, $password like you normally would */
    $con = mysql_connect($host, $username, $password);
    //connection to the database
    $dbhandle = mysql_connect($hostname, $username, $password) or die("Unable to connect to MySQL");
    echo "<br>Connected to MySQL</br>";
        
    sleep(10);
    //select a database to work with
    $selected = mysql_select_db("kauwpdb",$dbhandle) or die("Could not select kauwpdb");
    
    //execute the SQL query and return records
    $sql = mysql_query("SELECT * FROM city") ;//or die("Could not query database");

    echo "<table border='4' class='stats' cellspacing='0'>
    <tr><td class='hed' colspan='8'>Population of Major Cities</td></tr>
    <tr><th>ID</th><th>City Name</th><th>Population</th></tr>";
    //echo "ID:".$row{'id'}." Name:".$row{'model'}.
    
    while($rows = mysql_fetch_array($sql)) 
    { 
        echo "<tr>";
        echo "<td>" .$rows{'ID'}. "</td>";
        echo "<td>" .$rows{'Name'}. "</td>";
        echo "<td>" .$rows{'Population'}. "</td>";
        echo "</tr>";
    }
    echo "</table>";
    
    //close the connection
    //mysql_close($dbhandle);
?>
</body>
</html>
