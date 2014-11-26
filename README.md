GhostRunner
===========

Web based management of PhatomJS scripts.

This allows the storage and running of PhantomJS scripts (when combined with the server task running service). 

Scripts can be parameterized by entering vaiables using square bracket notation, e.g if you want to have the same script run using different usernames every time use something like [username] in the script and GhostRunner will prompt for it when ran.

This project really was made to familiarize myself with PhantomJS and write something new in .Net MVC. The app works in the following way.

<b>Create an account</b>

![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateAccount.png)

<b>Sign in</b>

![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SignIn.png)

<b>Project list</b>
This displays a list of projects that have been created in GhostRunner. You can create new projects here and it's just a way to group your scripts.

![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ProjectList.png)


Any server this is installed on will require .Net 4.5 and SQL Server.
