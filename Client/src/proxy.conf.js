const PROXY_CONFIG = [
  {
    context: [
      "/accountApi",
      "/securitysApi",
    ],
    target: "https://localhost:7252",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
