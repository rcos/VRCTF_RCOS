# Scenario 3: Fraud Detective

## Difficulty: Hard 

## Environment
A building with 4 floors: 
  - 1st Floor: Lobby
  - 2nd Floor: Sales Department
  - **3rd Floor: Accounting Department**
  - 4th Floor: IT Department


## Task
Find a way into the company's building using an employee's ID and search for this employee's room. This employee is currently investigating suspicious tax fraud in the company. Help them solve this case and unlock the CTF code at the end. 

## User Prompt

- Part 1: Find a way into the building. Use your surroundings to help you. 
- Part 2: Find the employee's room. Use your surroundings to help you. Remember to hold onto the ID.
- Part 3: Find clues on this employee's desk. What is the next location you can go to? Use your surroundings to help you. 
- Part 4: Find the hidden room and unlock the safe box. Use your surroundings to help you.

Note: These instructions will show up one at a time(after the user completes the previous part). 

## Interactivity
- **Employee's ID**: Can be picked up (and carried)
- **Building Door**: Can be opened after you put the ID near the scanner
- **Elevator**: Can be opened after pressing the up/down buttons
- **Employees' Office Door**: Can be opened after you type in the employee's PIN into the interactive keypad of the digital door lock
- **Ledger**: Can be picked up
- **Tax Return**: Can be picked up
- **Books**: Can be picked up
- **Invoice**: Can be picked up
- **Safe Box**: Can be opened after typing the right password on the interactive keypad.
  
## Solution
1) Use the dropped ID on the floor and scan it to open the building door. 
2) Take the elevator to the third floor and find this employee's office. 
3) There should be clues on the ledger and bulletin board on who the primary suspect is and where they are. Find this suspect's office and enter it. 
4) Examine the surroundings in the room and use clues to find the hidden room. Look for the CTF code and enter it in the safe box to unlock it. 

## Notes
Unlike the other challenges, we will incorporate interactive keypads(for unlocking doors/entering password for safe box) instead of a keyboard. Also, we need to add an additional interactability feature on the employee's ID, so the user can carry it, which might be hard to implement. Suggestion: Use an inventory system

## Script

**Setting**: User starts in front of a corporate building. 

**Step 1**: Look around in front of the building. There should be a dropped ID on the floor. Pick it up and scan it. Open the building door, which is now unlocked.

**Step 2**: After entering the building, look at the information on the ID. There should be a name, photo, job title, and PIN number. The name is Robert Haas(or any random name) and the job title is Senior Finincial Analyst, Accounting Department. 

**Step 3**: The lobby has a map directory. Take the elevator to the third floor, which leads to the accounting department. 

**Step 4**: Walk through the hallway and find this employee's office(Room 307). There should be nameplates outside each room. 

**Step 5**: The office door(s) has digital locks on them. Enter the employee's PIN to unlock the door.

**Step 6**: Inside the employee's office, there is a ledger on the desk and a bulletin board on the wall. 
- The ledger contains a chart with Date, Transaction ID, Description, Employee, Amount($), Notes.
- The bulletin board contains photos of the employees listed on the ledger and information about them(name, job title, PIN number).

**Step 7**: One line of the ledger is highlighted:
  - Date: 02/10/2025
  - Transaction ID: TXN-100999
  - Description: Consulting Fee Payment
  - Employee: Michael Chase(or any random name)
  - Amount($): 12, 000
  - Unusual vendor

**Step 8**: On the bulletin board, the photo of Michael Chase is circled in red. There are notes written below the photo.
  - Name: Michael Chase
  - PIN: 2222
  - Job: Sales consultant, Sales Department
  - Room: 202

**Step 9**: Exit the employee's room and head to the suspect's room. Take the elevator to the second floor and find room 202. Enter's the suspect's PIN(2222) into the keypad.

**Step 10**: The suspect's room has a desk and a bookshelf. There is the suspect's tax return on the desk, which has a sticky note. It says "The best lies are wrapped in truth.” – The Book of Fortune and Fraud

**Step 11**: Find the Book of Fortune and Fraud on the bookshelf and interact with it. When the book is pulled out, the bookshelf slides to the right to reveal a hidden room.

**Step 12**: Inside the hidden room, there is an invoice and a safe box on a desk. Scan through the information of the invoice, you will see there is a **Processing Code: CTF_92731** 

**Step 13**: The keypad on the safe box has both alphabetical and numerical buttons. Type in CTF_92731 and unlock the safe box.

**Step 14**: Inside the safe, there’s a note that says: 
“Congrats! You cracked the code. Too bad you’re a few steps behind. – M.C.” 

# **INVOICE**
**Company Name:** TBD &nbsp;&nbsp; | &nbsp;&nbsp; **Billing Address:** TBD &nbsp;&nbsp; 

**Invoice Number:** INV-583927  
**Date:** 02/10/2025  
**Due Date:** 03/10/2025  

## **Billed To:**  
**Michael Chase**  
Sales Department  
(Company Name) Ltd.  

---

## **Invoice Details:**  

| Item Description                           | Quantity | Unit Price  | Total Price  |
|-------------------------------------------|----------|------------|-------------|
| "Consulting Services - Special Project"  | 1        | $12,500.00 | $12,500.00  |
| "Data Analysis Report (Confidential)"    | 1        | $8,750.00  | $8,750.00   |
| "Internal Review Fees"                    | 1        | $6,000.00  | $6,000.00   |
| **Processing Code: CTF_92731**            | -        | -          | -           |

---

### **Total Amount Due:** **$27,250.00**  
**Payment Method:** Wire Transfer  
**Bank Details:** Account # XXXXXXX-XX | Routing # XXXXXXX  

---

### **Notes:**  
- This invoice must be processed by **March 10st, 2025**.  
- Contact **(Company Name) Finance Dept.** for any discrepancies.  
- **Internal Reference: "Special Project - Approved by M.C."**  