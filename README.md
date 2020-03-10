# GoSharp

A simple Go short link service, written in .NET Core.

This is still work in progress, but should work just fine.

# Run in Docker

To run inside of a Docker container, build it from `Dockerfile` (or `docker-compose.yml`), and run it. 

Storage of saved links will, by default, be stored at `data`. This can be changed by setting the `DATA` environment variable, like `docker run -e DATA="/otherdatapath" gosharp`. Use this to mount a volume to persist the links.

The container port is port `80`.
