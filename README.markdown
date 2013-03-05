# TrelloExcel

[Download version 0.4.0](https://bitbucket.org/dillenmeister/trelloexcel/downloads/TrelloExcel_0.4.0.zip).

An Excel Add-In that creates cards in [Trello](https://trello.com) from rows in Excel 2010 (Excel 2007 is not supported).

Follow progress on [this Trello board](https://trello.com/board/trelloexcel/4f74e95f90253b853b2b547b).

Supplemented by Nate Wagar in March 2013.  
Added support for time tracking, estimates, and checklists.  
Time Tracking and estimates are done by adding square brackets to card or checklist item names:  
[4/16] means 4 hours have been logged against a 16 hour estimate.  
If Checklists have a board title in curly braces in their title, they will be considered "relevant".  
Checklist {Develop} will only be imported if the card is in the "Develop" list.  
I also added an "Update cards" button. This copies over a spreadsheet range instead of shifting it down.
	
License: [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html)	
