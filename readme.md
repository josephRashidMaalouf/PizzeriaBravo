## Food-Stuffs Service

All responses are wrapped in a Response object following this scheme:

```
{
    "data" {},
    "isSuccess": bool
    "message": string
}
```

Foodstuff object:

```
{
    "id" : GUID,
    "name" : string,
    "price" : double
}
```

### 1. Get All Food-Stuffs

- **Endpoint**: `/api/food-stuffs`
- **Method**: `GET`
- **Description**: Retrieves a list of all food-stuff items.
- **Response**: JSON array of food-stuff items.

### 2. Get Food-Stuff by ID

- **Endpoint**: `/api/food-stuffs/{id}`
- **Method**: `GET`
- **Description**: Retrieves a specific food-stuff item by its ID.
- **Parameters**:
  - `id` (path): The unique identifier of the food-stuff item.
- **Response**: JSON object representing the food-stuff item.

### 3. Add New Food-Stuff

- **Endpoint**: `/api/food-stuffs`
- **Method**: `POST`
- **Description**: Adds a new food-stuff item.
- **Request Body**: JSON object representing the food-stuff item.
- **Response**: JSON object of the created food-stuff item.

### 4. Update Food-Stuff

- **Endpoint**: `/api/food-stuffs/{id}`
- **Method**: `PUT`
- **Description**: Updates an existing food-stuff item.
- **Parameters**:
  - `id` (path): The unique identifier of the food-stuff item.
- **Request Body**: JSON object representing the updated food-stuff item.
- **Response**: JSON object of the updated food-stuff item.

### 5. Delete Food-Stuff

- **Endpoint**: `/api/food-stuffs/{id}`
- **Method**: `DELETE`
- **Description**: Deletes an existing food-stuff item.
- **Parameters**:
  - `id` (path): The unique identifier of the food-stuff item.
- **Response**: HTTP 200 No Content on success.
