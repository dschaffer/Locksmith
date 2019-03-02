# Documentation

The documentation for this years Hackathon must be provided as a readme in Markdown format as part of your submission. 

You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.

Examples of things to include are the following.

## Summary

**Category:** Best enhancement to the Sitecore Admin (XP) UI for Content Editors & Marketers

In Sitecore environments that have multiple authors making changes to the same items authors are generally required to lock items before making edits to ensure there are no collisions between authors. One common downside with this approach is that authors need to remember to unlock their items once editing is complete, because if they don’t then no other users can lock and edit that item until the original editor or an admin logs in and unlocks it.

Our module (codename Locksmith) gives editors the ability to review changes field by field or in page context via a modal in the Sitecore admin so items left locked by other users after a period of time can be safely unlocked by any other user. This functionality will be accessed via a yellow notification bar on the locked item which appears only if the item is locked and: 
	•	the lock owner has logged out or their session has expired
	•	the lock owner is not admin (can be extended to specific roles as well)
	•	the item is normally editable/lockable during the current workflow state
	•	the user normally has write access to the item

Functionality we’d like to add to future releases: 
	•	Add support for more Sitecore field types (droplist, multiline text, checkbox, etc.)
	•	Allow original lock owner to revert individual field changes or the entire item
	•	Add icon to Review ribbon that launches the Lock Summary modal
	•	Create super role that can’t have its locks overridden
	•	HTML email sent to lock owner (instead of plain text)


## Pre-requisites

Does your module rely on other Sitecore modules or frameworks?

- List any dependencies
- Or other modules that must be installed
- Or services that must be enabled/configured

## Installation

Provide detailed instructions on how to install the module, and include screenshots where necessary.

1. Use the Sitecore Installation wizard to install the [package](#link-to-package)
2. ???
3. Profit

## Configuration

How do you configure your module once it is installed? Are there items that need to be updated with settings, or maybe config files need to have keys updated?

Remember you are using Markdown, you can provide code samples too:

```xml
<?xml version="1.0"?>
<!--
  Purpose: Configuration settings for my hackathon module
-->
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <settings>
      <setting name="MyModule.Setting" value="Hackathon" />
    </settings>
  </sitecore>
</configuration>
```

## Usage

![Screenshot of Notification Bar -- REPLACE IMAGE](images/hackathon.png?raw=true "Hackathon Logo")

If the 'Review Changes and Unlock' link in the yellow notification bar is clicked a Lock Summary modal is displayed that looks and feels like the Sitecore Validation Summary which should be familiar to most authors. 
Each row in the modal represents a field on the item that was edited, with the old value on the left and the new value on the right. 
The author then selects via radio buttons in either column which value to take when unlocking.

![Screenshot of Lock Summary in modal -- REPLACE IMAGE](images/hackathon.png?raw=true "Hackathon Logo")

Also in the modal, below the field changes summary, will be a list of other Sitecore items that refer to the locked item in their Presentation Details. 
Clicking on one of these links will open a new tab showing that associated item in Experience Editor mode if the associated item has a layout. 
Old and new field values can also be reviewed and selected via the Experience Editor UI.

When an editor’s review is complete and clicks "Update and unlock item" the item will be unlocked and saved with the selected field values. 
The original lock owner will be notified via email that their item has been unlocked and see a summary of the changed field values.

![Screenshot of email sent to lock owner -- REPLACE IMAGE](images/hackathon.png?raw=true "Hackathon Logo")

## Video

Please provide a video highlighing your Hackathon module submission and provide a link to the video. Either a [direct link](https://www.youtube.com/watch?v=EpNhxW4pNKk) to the video, upload it to this documentation folder or maybe upload it to Youtube...

[![Sitecore Hackathon Video Embedding Alt Text](https://img.youtube.com/vi/EpNhxW4pNKk/0.jpg)](https://www.youtube.com/watch?v=EpNhxW4pNKk)
