* ClojureCollections
* Copyright (c) Dr. Cornelius Mund. All rights reserved.
* The use and distribution terms for this software are covered by the
* Eclipse Public License 1.0 (http://opensource.org/licenses/eclipse-1.0.php)
* which can be found in the file epl-v10.html at the root of this distribution.
* By using this software in any fashion, you are agreeing to be bound by the
* terms of this license.
* You must not remove this notice, or any other, from this software.

ClojureCollections offers straight forward generic wrapper classes for the
Persistent collection types in clojure so these can be readily used in C#.
So far, PersistentVector, PersistentMap and PersistentList have been implemented.

For usage examples see the unit tests.

v.0.2
- Replaced IMapEntry with KeyValuePair
- Fixed bugs where functions would return a value from a map or a vector from a
  position which doesn't exist. Since structs and primitive types can be used as
  generics null cannot be returned in these cases. No an exception is thrown.
- Fixed bugs todo with peeping and poping empty lists/vectors. An exception is
  thrown in these cases now.
- Updated the documentation to C# style comments so it shows up in visual studio.

v0.1.1
- Fixed typo in IPersistentHashMap

v0.1
- Port of Java ClojureCollections to C#
