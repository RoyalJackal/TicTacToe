# TicTacToe Api Description

## Auth

### POST /Auth/login
Takes username and password, returns JWT token and it's expiration time.</br>
Input example:
```bash
{
  "username": "string",
  "password": "string"
}
```
Output example:
```bash
{
  "token": "string",
  "validUntil": "2023-03-26T23:38:21.143Z"
}
```

### POST /Auth/register
Takes user data, returns JWT token and it's expiration time.</br>
Input example:
```bash
{
  "username": "string",
  "email": "string",
  "password": "string"
}
```
Output example:
```bash
{
  "token": "string",
  "validUntil": "2023-03-26T23:38:21.143Z"
}
```

## Game

### POST /Game/create (Authorize)
Takes "X" or "O", creates a game lobby with a creator on a specified side, returns lobby info.</br>
Input example:
```bash
"X"
```
Output example:
```bash
{
  "id": 0,
  "row1": [
    "X"
  ],
  "row2": [
    "X"
  ],
  "row3": [
    "X"
  ],
  "xUserId": "string",
  "xUserName": "string",
  "oUserId": "string",
  "oUserName": "string",
  "turn": "X",
  "side": "X",
  "result": "XWin"
}
```

### POST /Game/join/{id} (Authorize)
Takes lobby id, user joins the lobby with a specified id if possible on a free side, returns lobby info.</br>
No body input.</br>
Output example:
```bash
{
  "id": 0,
  "row1": [
    "X"
  ],
  "row2": [
    "X"
  ],
  "row3": [
    "X"
  ],
  "xUserId": "string",
  "xUserName": "string",
  "oUserId": "string",
  "oUserName": "string",
  "turn": "X",
  "side": "X",
  "result": "XWin"
}
```

### POST /Game/turn/{id} (Authorize)
Takes lobby id and board coordinates, user places "X" or "O"(depending on his side) at specified coordinates if possible, returns lobby info.</br>
Input example:
```bash
{
  "x": 0,
  "y": 0
}
```
Output example:
```bash
{
  "id": 0,
  "row1": [
    "X"
  ],
  "row2": [
    "X"
  ],
  "row3": [
    "X"
  ],
  "xUserId": "string",
  "xUserName": "string",
  "oUserId": "string",
  "oUserName": "string",
  "turn": "X",
  "side": "X",
  "result": "XWin"
}
```

## Lobby

### GET /Lobby/all
Returns all lobbies.</br>
No input required.</br>
Output example:
```bash
[
  {
    "id": 0,
    "xUserId": "string",
    "xUserName": "string",
    "oUserId": "string",
    "oUserName": "string"
  }
]
```

### GET /Lobby/open
Returns all open lobbies.</br>
No input required.</br>
Output example:
```bash
[
  {
    "id": 0,
    "xUserId": "string",
    "xUserName": "string",
    "oUserId": "string",
    "oUserName": "string"
  }
]
```

### GET /Lobby/user (Authorize)
Returns all lobbies, in which current user is present.</br>
No input required.</br>
Output example:
```bash
[
  {
    "id": 0,
    "xUserId": "string",
    "xUserName": "string",
    "oUserId": "string",
    "oUserName": "string"
  }
]
```

## User

### GET /User/info/{id}
Takes user id, returns user info.</br>
No body input.</br>
Output example:
```bash
{
  "username": "string",
  "games": 0,
  "wins": 0
}
```
