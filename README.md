# Ntech.Caching

## Overview

Ntech.Caching is a .NET solution that provides both in-memory and distributed caching implementations. It's designed to offer flexible caching strategies for applications, improving performance and reducing database load.

## Features

*   **In-Memory Caching**: Utilizes .NET's built-in `IMemoryCache` for fast, application-local caching.
*   **Distributed Caching**: Implements `IDistributedCache` for scalable caching across multiple application instances, typically backed by a Redis server.
*   **Docker Support**: Includes Docker Compose configurations for easily setting up a Redis instance as a distributed cache store.
*   **Seed Data**: Provides a SQL script for initial database seeding.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

*   [.NET SDK](https://dotnet.microsoft.com/download) (Version compatible with .NET Core 3.1 or .NET 5/6+)
*   [Visual Studio](https://visualstudio.microsoft.com/downloads/) (or Visual Studio Code with C# extension)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop) (for distributed caching with Redis)

### Setup

1.  **Clone the Repository**:

    ```bash
    git clone https://github.com/your-repo/ntech.caching.git
    cd ntech.caching
    ```

2.  **Open in Visual Studio**:

    Open the `Ntech.Caching/Ntech.Caching.sln` solution file in Visual Studio.

3.  **Run Docker Compose (for Distributed Caching)**:

    Navigate to the `Caching/Ntech.Caching/Docker` directory in your terminal and run:

    ```bash
    docker-compose up -d
    ```

    This will start a Redis container in the background.

4.  **Build and Run**: Build the solution in Visual Studio. You can then run the individual projects (e.g., `Ntech.Caching.MemoryCache` or `Ntech.Caching.DistributedCache`) or integrate them into your application.

## Project Structure

*   `Caching/Ntech.Caching.MemoryCache/`: Contains the in-memory caching implementation.
*   `Caching/Ntech.Caching.DistributedCache/`: Contains the distributed caching implementation (likely using Redis).
*   `Caching/Ntech.Caching/Docker/`: Docker Compose files for setting up Redis.
*   `Caching/Ntech.Caching/seed.sql`: SQL script for database seeding.
*   `Caching/Tool/`: Contains `redis-desktop-manager.zip`, a tool for managing Redis.

## Usage

To use the caching functionalities, you would typically inject the appropriate caching interface (`IMemoryCache` or `IDistributedCache`) into your services and use its methods for `Get`, `Set`, `Remove`, etc. Refer to the specific project's code for detailed implementation examples.

## Seed Data (`seed.sql`)

The `seed.sql` file contains SQL commands to populate your database with initial data. This is useful for development and testing purposes.

## Redis Desktop Manager

Under `Caching/Tool/`, you will find `redis-desktop-manager.zip`. This is a convenient tool for visually managing and inspecting your Redis instances. 