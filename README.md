GhostRunner
===========

GhostRunner is an app that when installed on a server allows the web based management and running of JavaScript files when combined with the GhostRunner.Server service. The standalone version is at <a href="http://goghostrunner.com">http://goghostrunner.com</a> and the new hosted version is at <a href="http://ghostrunner.io">http://ghostrunner.io</a>

GhostRunner supports the definition of several script types (Git, Grunt, Node.js, PhantomJS and command line) which can be strung together as sequences. Sequences and scripts can be ran manually, be scheduled, or started via a POST request.

The purpose is to allow tasks that might not be wanted in the core of the application to be abstracted out or (as I am using it) as a continuous intergration server.

Scripts can be parameterized by entering variables using double square bracket notation, e.g. [[parameter_name]], which allows a script to be reused depending on the sequence it is required for. 

<p><b>Project list</b><br/>This displays a list of projects that have been created in GhostRunner. You can create new projects here and it is a great way to group your scripts.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ProjectList.PNG)
<br/>
<p><b>Create a script</b><br>Once inside a project you can define your scripts. They can be parameterized using double square bracket notation (like in this example [[username]] is defined so it can be specified each time the script runs).</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/CreateScript.PNG)
<br/>
<p><b>Script list</b><br>Once a script has been defined it will appear under the project and can be ran or edited as required.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptList.PNG)
<br/>
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptsDetail.PNG)
<br/>
<p><b>Run script</b><br>Pressing run on a script will allow you to specify the defined parameters and then will queue it for processing by the GhostRunner.Server.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScriptRun.PNG)
<br/>
<p><b>Create a sequence</b><br>Inside a project you can define a sequence of scripts. The scripts will be ran one after another when ran.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SequenceScripts.PNG)
<br/>
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SequenceScriptDetail.PNG)
<br/>
<p><b>Sequence list</b><br>Once a sequence has been defined it will appear under the project and can be ran or edited as required.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/SequenceList.PNG)
<br/>
<p><b>History</b><br>A record is made of of every time a script or sequence has ran along with its current status. expanding also gives all the details of the ran script or sequence.</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/History.PNG)
<br/>
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/HistoryDetail1.PNG)
<br/>
<br/>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/HistoryDetail2.PNG)
<br/>
<p><b>Schedule</b><br>Once you have scripts and sequences defined you can schedule them to run as required daily, weekly or monthly</p>
![alt tag](https://raw.githubusercontent.com/gmarland/GhostRunner/master/DemoImages/ScheduleList.PNG)
<br/>
<p><b>Remote Start</b><br>Sequences or scripts can be kicked off through a POST request to http://[ServerLocation]/post/task/[External ID of script or sequence]. The External ID can be found on the script or sequence definition of the app. Parameters for scripts can also be defined within the POST request as required</p>
<br/>
Any server this is installed on will require .Net 4.5. If you want some help setting this up or have any questions please feel free to contact me.
