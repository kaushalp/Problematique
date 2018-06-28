<html>
    <body>
<?php
    $connstr = getenv("MYSQLCONNSTR_mysqldb");    
    
    //echo "Coonection String: " .$connstr. "</br>";
    
    //Parse the above environment variable to retrieve username, password and hostname.
    foreach ($_SERVER as $key => $value) 
    {
        if (strpos($key, "MYSQLCONNSTR_mysqldb") !== 0) 
        {
            continue;
        }
        $dbname = preg_replace("/^.*Database=(.+?);.*$/", "\\1", $value);
        $hostname = preg_replace("/^.*Data Source=(.+?);.*$/", "\\1", $value);
        $username = preg_replace("/^.*User Id=(.+?);.*$/", "\\1", $value);
        $password = preg_replace("/^.*Password=(.+?)$/", "\\1", $value);
        break;
    }
    echo "Server Name: ".$hostname."</br>";
    echo "Database Name: ".$dbname."</br>";
    echo "User Name: ".$username."</br>";

    
    //connection to the database    
    $conn = new mysqli($hostname, $username, $password, $dbnames) or die("Unable to connect to MySQL");
    echo "<br>Connected to DB server successfully</br>";
    //select a database to work with
    //$selectDb = mysql_select_db("employees",$dbhandle) or die("Could not select database");
    
    $sql = "SELECT * FROM employees.employees LIMIT 1000";
    $result = $conn->query($sql);
    
    //execute the SQL query and return records
    if ($result->num_rows > 0) 
    {
    	echo "<table border='4' class='stats' cellspacing='0'>
        <tr><td class='hed' colspan='8'>Employee List</td></tr>
        <tr><th>Employee ID</th><th>First name</th><th>Last Name</th>
        <th>Date of birth</th><th>Date of Hire</th><th>Gender</th></tr>";    
        while($rows = $result->fetch_assoc()) 
        { 
            echo "<tr>";
            echo "<td>" .$rows{'emp_no'}. "</td>";
            echo "<td>" .$rows{'first_name'}. "</td>";
            echo "<td>" .$rows{'last_name'}. "</td>";
            echo "<td>" .$rows{'birth_date'}. "</td>";
            echo "<td>" .$rows{'hire-date'}. "</td>";
            echo "<td>" .$rows{'gender'}. "</td>";
            echo "</tr>";
        }
        echo "</table>";
	}
	else {
    echo "0 results";
    }
   // mysql_close($dbhandle);
?>
</body>
</html>
