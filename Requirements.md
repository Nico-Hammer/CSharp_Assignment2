# Pages
## Main page
- [x] Banner at the top of page with text "My Trip Log"
- [ ] Subheading that displays only if theres text in the ViewData property "Trip to {place} added." (this is done after the whole trip adding process is complete)
- [ ] Add Trip link should start a 3 page trip info entry process
## First info page
- [ ] Entry fields for destination, accommodation, start date, end date 
- [x] Destination and dates are mandatory but accommodation is optional
## Second info page
- [ ] Should only show up if the user entered something in the accommodation field of the last page
- [ ] Entry fields for phone number and email address
- [ ] Subheading should display "Add Info for {accommodation}
## Third info page
- [ ] Entry fields for 3 things to do
- [ ] Subheading should display "Add things to Do in {destination}
- [x] Entry fields are optional
# Behaviour
- [ ] When the user clicks the Next button on the first or second page the web app should save the data posted from the page in TempData
- [ ] When the user clicks the Save button on the third page the web app whould save the data posted from the page and the data in TempData to the database, then return to the home page with the subheading
- [ ] When the user clicks the Cancel button on any of the pages, the data in TempData should be cleared via `TempData.clear()` and the home page should be displayed
- [x] All fields for the trip should be stored in a single table