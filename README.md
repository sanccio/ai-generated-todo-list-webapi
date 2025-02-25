# Todo API

The Todo API allows you to manage a todo list through RESTful interactions.

## Running the Project

Start the project using the command `dotnet run`.

## Using the API

The API supports the following operations:
- `GET /api/todo` - retrieve all tasks.
- `GET /api/todo/{id}` - retrieve a task by ID.
- `POST /api/todo` - create a new task.
- `PUT /api/todo/{id}` - update a task by ID.
- `DELETE /api/todo/{id}` - delete a task by ID.

## Task Feedback

- **Ease of Completion with AI**: Easy.
- **Time Taken**: ~2 hours.
- **Code Readiness and Challenges Faced**:

    Overall, the generated business logic code was ready to run, with each endpoint functioning as expected.

    However, setting the task for the chat required multiple attempts to achieve the desired initial result. Additional refinements were made through clarifications during the dialogue to improve the accuracy of the service method logic.

    Challenges arose when generating tests for a service that interacts directly with DbContext, as ChatGPT produced non-functional code. Additionally, some assertions in the generated tests used outdated constructs.


- **Specific Prompts Learned**:

    `It is necessary to precisely execute the following task: [task text]`.

    `If it's not possible to send the entire code at once, break it up and ask me to make a request with the word "continue" for further generation.`.
