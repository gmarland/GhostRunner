GhostRunner
===========

Web based management of PhatomJS scripts.

This allows the storage and running of PhantomJS scripts (when combined with the server task running service). 

Scripts can be parameterized by entering vaiables using square bracket notation, e.g if you want to have the same script run using different usernames every time use something like [username] in the script and GhostRunner will prompt for it when ran.

This project really was made to familiarize myself with PhantomJS and write something new in .Net MVC. The app works in the following way.

<p><b>Authentication</b></p>
<table style="width: 100%">
  <tr>
    <td style="width: 50%">![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateAccount.png)</td>
    <td style="width: 50%">![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SignIn.png)</td>
  </tr>
</table>
<br/>
<p><b>Project list</b><br/>This displays a list of projects that have been created in GhostRunner. You can create new projects here and it's just a way to group your scripts.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ProjectList.png)
<br/>
<p><b>Create a script</b><br>Inside a project you can define a PhantomJS script. This can be parameterized using square bracket notation (like in this example [username] is defined so it can be specified each time the script runs).</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateScript.png)
<br/>
<p><b>Script list</b><br>Once a script has been defined it will appear under the project and can be ran or edited as required.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptList.png)
<br/>
<p><b>Run script</b><br>Pressing run on a script will allow you to specify the defined parameters and the will queue it for processing by the GhostRunner.Server.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/RunScript.png)
<br/>
<p><b>Scripts ran</b><br>A record of every time the script has ran will be stored under the main script header with its current status.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptsRan.png)
<br/>
<p><b>Script details</b><br>You can also drill into the script and see parameters defined, the details of the script used and a log of the output from PhantomJS.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptOutput1.png)
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptOutput2.png)

Any server this is installed on will require .Net 4.5 and SQL Server. If you want some help setting this up or have any questions please feel free to contact me.
