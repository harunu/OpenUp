# OpenUp
OpenUpAssignment


Updates and Feature Enhancements
Overview
This release includes updates to Entity Framework migrations to address specific errors encountered during schema changes. Some refactoring was performed to enhance code structure, and partial exception handling logic was added to the backend for better error management. Additionally, new API endpoints and features have been implemented to streamline functionality, especially around editing availability and managing psychologist IDs.

## Backend Updates

1. Edit Availability - New Endpoint
To support deleting available time slots in the Edit Availability section, the following endpoint has been introduced:

### Delete Available Time Slot
This endpoint allows the removal of a specific time slot by its timeSlotId for a given psychologist.

**Endpoint:**  
`DELETE /Psychologist/{id}/available-timeslots/{timeSlotId}`

**Path Parameters:**
- `id` (string): The unique identifier of the psychologist.
- `timeSlotId` (string): The unique identifier of the time slot to be deleted.

**Example Request:**
```http
DELETE /Psychologist/12345/available-timeslots/53bb2aaa-a1a5-48f2-b78c-31b204783833 HTTP/1.1
Host: localhost:5001;
```

2. Accessing Psychologist IDs as a List

A new endpoint has been implemented to retrieve a list of all psychologist IDs:
```http
GET /Psychologist/ids
```
This endpoint provides a list of psychologist IDs, which can be fetched once during the initial frontend load. The IDs are used to populate a dropdown for easier selection in the dashboard.

3. Seed Data for Improved Dashboard Experience
To simplify the process during development, seed data is regenerated for each application run. You can view and select valid psychologist IDs without querying the database directly. The updated IDs will automatically populate the dropdown, enabling a seamless dashboard experience.

## Frontend Integration
Configurable Base URL

The backend base URL (localhost:5001) is configurable in axiosInstance.js. This allows easy adaptation to different environments.

Dependencies and Start

Run the following commands to set up the frontend:

```bash
npm install
npm start
```
Psychologist ID Dropdown

On the first load, the frontend fetches available psychologist IDs via the /Psychologist/ids endpoint. This allows users to select IDs directly from a dropdown.
Feature: Upcoming Sessions
To display upcoming sessions on the dashboard:

Book a Session

Use the following API endpoint:
POST /Client/{id}/bookings
Sample Payload:

{
  "psychologistId": 101,
  "availableTimeSlotId": "53bb2aaa-a1a5-48f2-b78c-31b204783833"
}
The availableTimeSlotId can be retrieved from the AvailableTimeSlotsOfPsychologists table.
The ClientId  can be retrieved from the Clients table.

Database Updates

Once the booking is successful, the record will be added to the BookedAppointmentsOfPsychologists table.
Deleting records will automatically update the UI and reflect the changes dynamically.

## Database Setup
For the backend:

Run the following command to apply migrations:
```bash
dotnet ef database update
```

This will initialize the database and apply the necessary schema changes.

Use Swagger UI to test and verify API endpoints:

Access Swagger at 
```http 
http://localhost:5001/swagger
```
## Known Limitations
Automatic client booking through the frontend is not yet implemented. However, you can manually select a valid ClientId from the database and create bookings using the POST /Client/{id}/bookings endpoint.

The availableTimeSlotId must be manually fetched from the AvailableTimeSlotsOfPsychologists table to book a session.

## Additional Notes
Frontend Screenshot: Below is an example screenshot of the application running locally with a configured localhost:5001 backend.
Dynamic Record Updates: Deleting bookings via the dashboard triggers backend updates and synchronizes the changes in real-time.
Exception handling logic for certain edge cases is partially implemented and can be refined in future releases.

Example Flow for Booking and Displaying Sessions
Select a valid Psychologist ID from the dropdown (fetched via /Psychologist/ids).
Use Swagger or a similar tool to create a booking with the POST /Client/{id}/bookings endpoint.

<img width="1712" alt="Screenshot 2025-01-24 at 08 18 53" src="https://github.com/user-attachments/assets/7d082205-aa30-46d9-bade-48bf467c2382" />

Verify the booking by checking the BookedAppointmentsOfPsychologists table.
The upcoming sessions will  populate the dashboard after you enter the ID again from dropdown. You can delete bookings directly, and changes will reflect dynamically.


Docker Support:
A docker-compose.yml file was not created due to time constraints, so running the application with docker-compose up is not yet available.

CI/CD:
Continuous Integration and Deployment (CI/CD) processes have not been implemented.

Areas for Improvement:

Significant refactoring is needed across both the backend and frontend.
Missing unit tests and improved exception handling.
Logging and bug fixing require attention.
On the frontend, implementing Redux for better state management is recommended.

## Screenshots

<img width="1712" alt="Screenshot 2025-01-24 at 08 21 08" src="https://github.com/user-attachments/assets/d5a22647-9b99-429e-a751-1218265f9363" />
<img width="1709" alt="Screenshot 2025-01-24 at 09 03 32" src="https://github.com/user-attachments/assets/61871140-9bc0-4a2d-a71f-a18d0d456172" />

