# Maintenance
  
Custom Maintenance - Minimal cleanup just for those maintenance tasks that you repeat regularly.  
  
Donations go a long way https://www.patreon.com/xCONFLiCTiONx
  
  
<a name="help"></a>
**HELP SECTION**
  
**Schedule Tasks:**  
  
You can set scheduled tasks to run whenever you like. The following is recommended:  

- Run at logon: Arguments: "/Logon" (no quotes)  - Set Name to Maintenance - Logon **This just does a quick cleanup**

- If you shut off your computer every night but leave it on once in a while for maintenance: Run Nightly: Arguments: "/FullCheckup /PuranFD C: -s1" (no quotes - REQUIRES Puran Defrag)  - Set Name to Maintenance - Nightly **This is Heavy maintenance**  

- If you leave your computer on every night: Run once or twice a month: Arguments: "/FullCheckup /PuranFD C: -s1" (no quotes - REQUIRES Puran Defrag)  - Set Name to Maintenance - Monthly **This is Heavy maintenance**  
  
  
**GUI:**  

- Directories To Delete: Delete a directory and all the contents

- Files To Delete: Delete specific files

- Files To Hide: Hide specific files

- Path Files To Delete: Delete all files in a folder but not the folder itself

- Path Files To Delete Older: Delete all files in a folder but not the folder itself older than a set number of days - Proper use: 1, path (1 = 1 day, path = path to folder containing these files)

- Services To Disable: Disables the services

- Services To Manual: Sets the services to Manual (allows them to still be ran but only as required by the system/program)

- Tasks To Disable: Disables scheduled tasks
  
  
**TIPS:**  
  
- You can add text to the combo box and press enter and it will be added