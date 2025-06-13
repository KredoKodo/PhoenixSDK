# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

- This section will be used for tracking changes that are not yet released.  Nothing to see here yet!

### Not Implemented

- Support for Phoenixd Notifications (Websockets, Webhook, Webhook authentication)

### TODO
- There are no unit tests yet, but they will be added in the future.
- Better error handling.
- Better documentation.
- Better validation of inputs and outputs.

## [1.0.3] - 2025-06-13

### Added
- N/A (no new features added in this release)

### Changed
- N/A (no changes made in this release)
 
### Fixed
- Fixed a bug introduced in the previous version regarding string validation for optional parameters.

## [1.0.2] - 2025-06-10

### Added
- Added LNURL support for LNURL Pay, LNURL Withdraw, and LNURL Auth.
- Added example code for LNURL Pay, LNURL Withdraw, and LNURL Auth in the Example Usage App.

### Changed
- Simplified the codebase by removing duplicate validation code and replacing it with a single validation class.
- Updated file structure for the logo, README, and changelog to be more organized.
 
### Fixed
- N/A (no bugs reported in this release)

## [1.0.1] - 2025-06-09

### Added
- First release of the KredoKodo PhoenixdSdk!

### Changed
- N/A (initial release)

### Fixed
- N/A (initial release)

---

## Release Notes

### Version 1.0.3
Fixed a bug that was introduced in the previous version that caused null check errors when optional parameters were not provided.  The string validation class was updated and renamed to fix this issue.

### Version 1.0.2
Added LNRUL support for LNURL Pay, LNURL Withdraw, and LNURL Auth.  The codebase has been simplified by removing duplicate validation code and replacing it with a single validation class.  The file structure for the logo, README, and changelog has been updated to be more organized.  No bugs were reported in this release.

### Version 1.0.1
This is the first release of the KredoKodo PhoenixdSdk.  It should be considered an alpha release, as it is not fully tested and does not yet have unit tests.  However, it is fully functional and can be used to interact with the Phoenixd Server API.