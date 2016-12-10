# PIXOL
This is a simple console application witten in C#, to download and back up the photos from a public as well as private account.

##How To Run
1) Open the folder containing project.
2) Goto ".../Pixol/Pixol/bin/Debug/" folder.
3) Open the command prompt here.
4) Run Pixol.exe with approrpiate account type specifier as arguments.

##Requirments
The folder in which the Pixol.exe file is residing must have following files for program to work.
1) RestSharp.dll
2) Newsoft.Json.dll
3) Pixol.exe.config

##Compulsory Arguments
a)   "-public" [username] for public account.

e.g: /Pixol.exe -public rockleeofleaf

b)   "-private" [username] [password] for private account.

e.g: /Pixol.exe -private rockleeofleaf aevolw143..

##Optional Arguments

"-likes" [minimum likes] specifier can be used to restrict the program to download only images
which have likes greater than equal to specified number.

e.g  "Pixol -pubic rockleeofleaf -likes 20
(this command will download all the images which have like >= 20)

##Dummy Account For Testing
Username=> rockleeofleaf

Password=> aevolw143..

Note:=> As the app is in sandbox mode private account can download 20 most recent images.
