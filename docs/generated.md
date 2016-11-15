# Rhythm.Core

<table>
<tbody>
<tr>
<td><a href="#collectionextensionmethods">CollectionExtensionMethods</a></td>
<td><a href="#stringsplitdelimiters">StringSplitDelimiters</a></td>
</tr>
<tr>
<td><a href="#stringextensionmethods">StringExtensionMethods</a></td>
</tr>
</tbody>
</table>


## CollectionExtensionMethods

Extension methods for collections.

### MakeSafe\`\`1(items)

Converts a null collection into an empty collection.

#### Type Parameters

- T - The type of item stored by the collection.

| Name | Description |
| ---- | ----------- |
| items | *System.Collections.Generic.IEnumerable{\`\`0}*<br>The collection of items. |

#### Returns

An empty list, if the supplied collection is null; otherwise, the supplied collection.


## StringSplitDelimiters

The types of delimiters that can be used to split strings.

### Comma

Split by commas.

### Default

Default will split by common delimiters (e.g., commas, line breaks, semicolons).

### LineBreak

Split by line breaks.

### Semicolon

Split by semicolon.


## StringExtensionMethods

Extension methods for strings.

### #cctor

Static constructor.

### CleanItems(items)

Cleans the collection of strings, trimming whitespace and removing empties.

| Name | Description |
| ---- | ----------- |
| items | *System.Collections.Generic.IEnumerable{System.String}*<br>The items to clean. |

#### Returns

The cleaned strings.

### SplitBy(source, delimiter)

Splits a string by the specified delimiter.

| Name | Description |
| ---- | ----------- |
| source | *System.String*<br>The string to split. |
| delimiter | *Rhythm.Core.Enums.StringSplitDelimiters*<br>Optional. The delimiter. If unspecified, default delimiters will be used. |

#### Returns

The split strings.

### SplitByChars(source, characters)

Split a string by the specified characters.

| Name | Description |
| ---- | ----------- |
| source | *System.String*<br>The string to split. |
| characters | *System.Char[]*<br>The characters to split by. |

#### Returns

The split strings.

### SplitByLineBreaks(source)

Split a string by line breaks.

| Name | Description |
| ---- | ----------- |
| source | *System.String*<br>The string to split. |

#### Returns

The split strings.
