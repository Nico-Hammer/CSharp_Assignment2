# Pages
## Main page
- [x] Banner at the top of page with text "My Trip Log"
- [x] Subheading that displays only if theres text in the ViewData property "Trip to {place} added." (this is done after the whole trip adding process is complete)
- [x] Add Trip link should start a 3 page trip info entry process
## First info page
- [x] Entry fields for destination, accommodation, start date, end date 
- [x] Destination and dates are mandatory but accommodation is optional
## Second info page
- [x] Should only show up if the user entered something in the accommodation field of the last page
- [x] Entry fields for phone number and email address
- [x] Subheading should display "Add Info for {accommodation}"
## Third info page
- [x] Entry fields for 3 things to do
- [x] Subheading should display "Add things to Do in {destination}"
- [x] Entry fields are optional
# Behaviour
- [x] When the user clicks the Next button on the first or second page the web app should save the data posted from the page in TempData
- [x] When the user clicks the Save button on the third page the web app whould save the data posted from the page and the data in TempData to the database, then return to the home page with the subheading
- [x] When the user clicks the Cancel button on any of the pages, the data in TempData should be cleared via `TempData.clear()` and the home page should be displayed
- [x] All fields for the trip should be stored in a single table