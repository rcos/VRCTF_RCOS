# Scenario 5: Multiroom

## Difficulty: Hard

## Environment
A building with 4 rooms: 
  - 1st Floor: Lobby
  - 2nd Floor: Hallway
  - 3rd Floor: Office

## Task
Find a way into the a office in a company building to find a sensitive document. Use surrounding objects, social enviroments, and deductive reasoning.

## User Prompt

- Good luck, use what you've learned!

## Interactivity
- **Employee's ID**: Can be inspected and put in inventory. The name is visible when inspected.
- **Lobby Couch**: Can be sat on (lower camera).
- **Reception Computer**: Can attempt to login into, but is a red herring and there is no correct password.
- **Keycard Door**: Can only be opened with **Employee's ID** object in/from inventory. Will send user to hallway scene.
- **Filing Cabinet**: Can be opened.
- **Document**: Can be inspected. Shows rules for office passcodes.
- **Office Door Keypad Lock** Can be interacted to spawn keyboard to enter password.
- **Employees' Office Door**: Can be only be opened when passcode flag is raised. Will send user to office room scene.
  
## Solution
1) Pick up keycard nearby lobby couches to enter the hallway through the locked door.
2) Find correct office door with matching nameplate to the name shown on the keycard. 
3) Open cabinet to find document with rules to keypad password rules.
4) Use rules to enter password 5409.
5) Place laptop near server and connect cable from server to laptop.

## Notes
Unlike the other challenges, we will incorporate interactive keypads(for unlocking doors/entering password for safe box) instead of a keyboard. Also, we need to add an additional interactability feature on the employee's ID, so the user can carry it, which might be hard to implement. Suggestion: Use an inventory system

## Script

**Setting**: User starts in lobby of a corporate building. 

**Step 1**: Look around couches in the lobby. There should be a dropped ID on the floor. Pick it up and use it access the inner hallways.

**Step 2**: After entering the hallway, look at the information on the ID. Find the office with the same name on the ID. Used numbers show wear on the keypad.

**Step 3**: Open filing cabinet to find document with keypad rules. Sum of first two digits must equal sum of last two digits.

**Step 4**: Enter password based on passcode rules and worn keys.

**Step 4**: Place laptop near server and connect correct cables.


## Brainstorming Notes

worn buttons/numbers

board - Required patterns (first number(s) must be x because y rule)

hidden file cabinent/computer - credit card security (xy = yz)

Wait for the person to enter the numbers to see order.

Physically break it open (bang or shimming) nice pitfall.

Assuming auto locks, wait for the guy to leave saying "bathroom break" and jam door before it closes (sound effect)

Clean floor to act like a janitor

Security camera placement awareness