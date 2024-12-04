# Azure ServiceBus Local Emulator Demo

This project demonstrates how to use an Azure ServiceBus Local Emulator. It includes multiple projects to showcase the functionality and configuration of the emulator.

## Setup

1. **Environment Configuration**:
   - Create a `.env` file in the root directory of the project.
   - Use the `.env-sample` file as a reference for the required environment variables.

2. **Start the Emulator**:
   - Run the following command to start the emulator using Docker Compose:
     ```sh
     docker-compose up
     ```

3. **Run the Demo**:
   - Start the two Order projects to see the demo in action.
   - The Configurator project demonstrates how to produce the necessary configuration for the emulator.

## Projects

- **Order Projects**: These projects simulate sending and receiving messages using the Azure ServiceBus Local Emulator.
- **Configurator Project**: This project shows how to generate the configuration required for the emulator.