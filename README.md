# GoSharp

A simple Go short link service, written in .NET Core.

This is still work in progress, but should work just fine.

# Setup

To setup, you might need a combination of an A-record, and a local search domain to respond correctly. You could set it up to react to "go", and forward it to the service.

You should properly research it some more yourself.

# Usage

Once you have set it up, you can add/edit a short URL with `go/edit/some-short-url`, or just go (pun intended) to `go/` and it will create a random address for you. If you go to an address that doesn't exist, it will ask where you want it to go. 

To be redirected to the URL, visit `go/some-short-url`.

If you need to see all links created, and how many times they have been visited, see `go/links`.

# Run in Docker

To run inside of a Docker container, build it from `Dockerfile` (or `docker-compose.yml`), and run it. 

Storage of saved links will, by default, be stored at `data`. This can be changed by setting the `DATA` environment variable, like `docker run -e DATA="/otherdatapath" gosharp`. Use this to mount a volume to persist the links.

The container port is port `80`.
