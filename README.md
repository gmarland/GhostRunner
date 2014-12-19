GhostRunner
===========

GhostRunner is an app that when installed on a server allows the web based management of PhantomJS scripts.

It allows for the definition, storage and running of PhantomJS scripts when combined with the GhostRunner.Server service. 

Scripts can be parameterized by entering variables using square bracket notation, e.g if you want to have the same script run using different usernames every time define [username] in the script and GhostRunner will prompt for it when ran. 

Sequences of scripts can also be defined that will run one after another once defined.

Finally, you can schedule scripts or sequencs to run daily, weekly or monthly as required.

This project really was made to familiarize myself with PhantomJS and hopefully will be useful for people using PhantomJS. The app works in the following way.

<p><b>Authentication</b></p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SignIn.png)
<br/>
<p><b>Project list</b><br/>This displays a list of projects that have been created in GhostRunner. You can create new projects here and it is a great way to group your scripts.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ProjectList.png)
<br/>
<p><b>Create a script</b><br>Inside a project you can define a PhantomJS script. This can be parameterized using square bracket notation (like in this example [username] is defined so it can be specified each time the script runs).</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateScript.png)
<br/>
<p><b>Script list</b><br>Once a script has been defined it will appear under the project and can be ran or edited as required.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptList.png)
<br/>
<p><b>Run script</b><br>Pressing run on a script will allow you to specify the defined parameters and then will queue it for processing by the GhostRunner.Server.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/RunScript.png)
<br/>
<p><b>Create a sequence</b><br>Inside a project you can define a sequence of PhantomJS scripts. The scripts will be ran one after another once ran.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateScript.png)
<br/>
<p><b>Sequence list</b><br>Once a sequence has been defined it will appear under the project and can be ran or edited as required.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SequenceList.png)
<br/>
<p><b>History</b><br>A record is made of of every time a script or sequence has ran along with its current status. expanding also gives all the details of the ran script or sequence.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/History.PNG)
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/HistoryDetail1.PNG)
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/HistoryDetail2.PNG)
<br/>
Any server this is installed on will require .Net 4.5 and SQL Server. If you want some help setting this up or have any questions please feel free to contact me.
