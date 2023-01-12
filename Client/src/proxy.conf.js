const PROXY_CONFIG = [
  {
    context: [
      "/userApi",
    ],
    target: "https://localhost:7252",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
