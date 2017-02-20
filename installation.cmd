Solution was developed using VS 2012 and Sql Server 2008. Please note that database project can be switched to target other databases such as Sql server 2005,
2012, 2014.

Installation instruction:
1. Build solution to restore Nuget packages
2. Right click "ArtistDB" project and publish if you have sql server 2008 installed. 

If a different version of Sql server is installed:
2.1. Right click "ArtistDB" project and select "Properties"
2.2. On "Project Settings" section, select Sql "Target Platform" and click save
2.3. Right click "ArtistDB" project and publish

3. Run project and test 
