using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// The Bag (i.e. multiset) class manages a dynamic array that grow and shrink as elements are added and removed.
/// </summary>
/// <typeparam name="T">Data type of the elements of the set.</typeparam>
[Serializable]
public class Bag<T> : IEnumerator<T>, IEnumerable<T>, ISerializable
{
    #region DATA
    /// <summary>Buffer size.</summary>
    protected int buffer;
    /// <summary>Array of elements.</summary>
    protected T[] array;					// System.Array.
    /// <summary>Number of allocated elements.</summary>
    protected int Max;
    /// <summary>Number of used elements.</summary>
    protected int Num;
    /// <summary>Default element value.</summary>
    protected T dval = default(T);
    /// <summary>Random number generator.</summary>
    protected Random random = new Random();

    protected int enumerationPosition = -1;
    #endregion

    #region CONSTRUCTION
    /// <summary>
    /// Default constructor with initial size 30 and buffer count 30.
    /// </summary>
    public Bag()
    {
        array = null; buffer = 30;
        Num = 0; Max = 30;

        array = new T[Max];
    }
    /// <summary>
    /// Constructor with initial buffer size.
    /// </summary>
    /// <param name="max">Initial array capacity.</param>
    public Bag(int max)
    {
        array = null; buffer = 30;
        Num = 0; Max = max;

        array = new T[Max];
    }
    /// <summary>
    /// Create a dynamic array from a standard array.
    /// </summary>
    /// <param name="a">Source array.</param>
    public Bag(T[] a)
    {
        buffer = 30;
        Max = (a == null) ? 0 : a.Length; Num = 0;

        array = new T[Max];

        for (int i = 0; i < Max; i++) array[Num++] = a[i];
    }
    /// <summary>
    /// Copy constructor.
    /// </summary>
    /// <param name="a">Source list.</param>
    public Bag(Bag<T> a)
    {
        buffer = 30;
        Num = (a == null) ? 0 : a.Length; Max = Num;

        array = new T[Max];

        for (int i = 0; i < Num; i++) array[i] = a.array[i];
    }
    /// <summary>
    /// Destructor.
    /// </summary>
    ~Bag()
    {
        array = null;
        Num = 0;
        Max = 0;
    }
    #endregion		// CONSTRUCTION

    #region MEMORY OPERATIONS
    /// <summary>
    /// Deallocate memory.
    /// </summary>
    protected void Delete()
    {
        array = null;
        Num = 0;
        Max = 0;
    }
    /// <summary>
    /// Empty the array.
    /// </summary>
    public void Clear()
    {
        Delete(); Num = 0; Max = buffer;
        array = new T[Max];
    }
    /// <summary>
    /// Expand and reallocate.
    /// </summary>
    /// <param name="size">Expansion count.</param>
    protected void Expand(int size)
    {
        if (size < 1) return;
        T[] temp = new T[Max + size];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max += size;
    }
    /// <summary>
    /// Free unused memory.
    /// </summary>
    protected void Reduce()
    {
        T[] temp = new T[Num];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max = Num;
    }
    #endregion

    #region PROPERTIES
    /// <summary>
    /// Get or set the item at an index.
    /// </summary>
    /// <param name="i">Index of an item.</param>
    /// <returns>The item at the index.</returns>
    public virtual T this[int i]
    {
        get
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();
            return array[i];
        }
        set
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();
            array[i] = value;
        }
    }
    /// <summary>Get or set the memory buffer.</summary>
    public int Buffer { set { buffer = value; } get { return buffer; } }
    /// <summary>Get a copy of the internal System.Array.</summary>
    public T[] Array
    {
        get
        {
            T[] clone = new T[Num];
            for (int i = 0; i < Num; i++) clone[i] = array[i];
            return clone;
        }
    }
    /// <summary>Get the number of items added to the collection.</summary>
    public int Length { get { return Num; } }
    /// <summary>Get the size of the collection including empty elements.</summary>
    public int Size { get { return Max; } }
    public T DefaultValue { set { dval = value; } get { return dval; } }
    #endregion

    #region OPERATORS
    /// <summary>
    /// Equality operator. Test if two arrays have identical content, including the order of elements.
    /// </summary>
    /// <param name="a">Array to compare.</param>
    /// <param name="b">Array to compare to.</param>
    /// <returns>True if both arrays contain the same values in the same order and
    /// the number of values are identical.</returns>
    public static bool operator ==(Bag<T> a, Bag<T> b)
    {
        if (ReferenceEquals(a, b)) return true;
        else if (ReferenceEquals(a, null)) return false;
        else if (ReferenceEquals(b, null)) return false;
        else if (a.Num != b.Num) return false;

        for (int i = 0; i < a.Num; i++)
        {
            if (!a.array[i].Equals(b.array[i]))
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// Inequality operator. Test if two sets are not identical.
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if all items are not identical in value and order.</returns>
    public static bool operator !=(Bag<T> a, Bag<T> b)
    {
        if (ReferenceEquals(a, b)) return false;
        else if (ReferenceEquals(a, null)) return true;
        else if (ReferenceEquals(b, null)) return true;
        else if (a.Num != b.Num) return true;

        for (int i = 0; i < a.Num; i++)
        {
            if (!a.array[i].Equals(b.array[i]))
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Appends one item to the end of the array.
    /// </summary>
    /// <param name="a">Set to append to.</param>
    /// <param name="b">Item to append.</param>
    /// <returns>An array with all items of a and b.</returns>
    public static Bag<T> operator +(Bag<T> a, T b)
    {
        Bag<T> sum = new Bag<T>(a);
        sum.Append(b);
        return sum;
    }
    /// <summary>
    /// Appends the elements of the second operand to the end of the first.
    /// </summary>
    /// <param name="a">First source array.</param>
    /// <param name="b">Second source array.</param>
    /// <returns>An array combined by the two arrays.</returns>
    public static Bag<T> operator +(Bag<T> a, Bag<T> b)
    {
        Bag<T> sum = new Bag<T>(a);
        sum.Append(b);
        return sum;
    }
    #endregion	// OPERATORS

    #region SEARCH
    /// <summary>
    /// Determine if a value is in the list.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">Search value.</param>
    /// <returns>True if found. False otherwise.</returns>
    public bool Has			( T e )
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if a given array is a subarray of this array.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Array to check.</param>
    /// <returns>True if all items of a appear in an unbroken sequence in this array.</returns>
    public bool Has			( Bag<T> a )
    {
        for (int i = 0; i <= Num - a.Num; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Num; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if any one item of a set appears in this array.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="set">Items to find.</param>
    /// <returns>True if at least one item appear this array.</returns>
    public bool HasAny		( params T[] set )
    {
        for (int i = 0; i < set.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set[i].Equals(array[j])) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if the set of items of an array intersects with the items of this array, disregarding order.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="set">Set of items.</param>
    /// <returns>True if at least one item is a member of both arrays.</returns>
    public bool HasAny		( Bag<T> set )
    {
        for (int i = 0; i < set.Num; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j])) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="set">Items to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool HasAll		( params T[] set )
    {
        for (int i = 0; i < set.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (set[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="set">Item to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool HasAll		( Bag<T> set )
    {
        for (int i = 0; i < set.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Find the index of the first occurence of an item.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">Searched item.</param>
    /// <returns>An index or -1 if the item is not in the array.</returns>
    public int FindFirst	( T e )
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the start index of the first subarray matching the given array.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Subarray to match</param>
    /// <returns>An index or -1 if no match is found.</returns>
    public int FindFirst	( Bag<T> a )
    {
        for (int i = 0; i <= Num - a.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Length; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) return i;
        }

        return -1;
    }
    /// <summary>
    /// Find the index of the last occurence of an item.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">Searched item.</param>
    /// <returns>An index if found, -1 otherwise.</returns>
    public int FindLast		( T e )
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (array[i].Equals(e)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the start index of the last subarray matching the given array.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Subarray to find.</param>
    /// <returns>An index or -1 if no match is found.</returns>
    public int FindLast		( Bag<T> a )
    {
        for (int i = Num - a.Num; i >= 0; i--)
        {
            bool match = true;
            for (int j = 0; j < a.Length; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) return i;
        }

        return -1;
    }
    /// <summary>
    /// Find the index of the first occurence of an item starting the
    /// search at index b.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index of the search.</param>
    /// <param name="e">Item to find.</param>
    /// <returns>True if the item was found (its index output to b), false otherwise.</returns>
    public bool FindNext	( ref int b, T e )
    {
        for (int i = b; i < Num; i++)
        {
            if (array[i].Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
	/// <summary>
	/// Find the index of the first occurence of an item starting the
    /// search at index b.
	/// </summary>
	/// <param name="b">Start index of the search.</param>
	/// <param name="e">Item to find.</param>
	/// <returns>An index to the item or -1 if not found.</returns>
	public int  FindNext	( int b, T e )
	{
		for (int i = b; i < Num; i++)
        {
            if (array[i].Equals(e))
            {
                return i;
            }
        }
		return -1;
	}
    /// <summary>
    /// Find the start index of the first occurence of a subarray starting the
    /// search at index b.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="a">Array to find.</param>
    /// <returns>True if a subarray was found (its index output to b), false otherwise.</returns>
    public bool FindNext	( ref int b, Bag<T> a )
    {
        for (int i = b; i <= Num - a.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Length; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) { b = i; return true; }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the index of the first occurence of the given element,
    /// starting the search at b and going backwards (towards lower
    /// indeces).
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index of search</param>
    /// <param name="e">Searched item.</param>
    /// <returns>True if the item was found, false otherwise. Its index or -1 is
    /// stored in the ref parameter.</returns>
    public bool FindPrior	( ref int b, T e )
    {
        for (int i = b; i >= 0; i--)
        {
            if (array[i].Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the start index of the last sequence ending before or at the 
    /// given index, searching towards lower indexes.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">The search starts at b+1-log.Length.</param>
    /// <param name="log">Sequence of items to find.</param>
    /// <returns>True if the sequence was found, false otherwise. Its start index
    /// is stored in the ref parameter.</returns>
    public bool FindPrior	( ref int b, Bag<T> log )
    {
        for (int i = b + 1 - log.Num; i >= 0; i--)
        {
            bool match = true;
            for (int j = 0; j < log.Num; j++)
            {
                if (!log.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the first element from the given index that does not match any of
    /// the given values, but is preceded by one such.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="e">Values find and skip past.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool FindPast	( ref int b, params T[] e )
    {
        int ie = -1; // index of the last element matching any of the parameter values.

        for (int i = b; i < Num; i++)
        {
            for (int j = 0; j < e.Length; j++)
            {
                if (array[i].Equals(e[j]))
                {
                    ie = i;
                    break;
                }
                else if (ie != -1)
                {
                    b = i;
                    return true;
                }
            }
        }

        b = -1;
        return false;
    }
	/// <summary>
	/// Find the first element from the given index that does not match any of
    /// the given values, but is preceded by one such.
	/// </summary>
	/// <param name="b">Beginning index of search.</param>
	/// <param name="e">Values find and skip past.</param>
	/// <returns>An index or -1 if not found.</returns>
	public int  FindPast	( int b, params T[] e )
	{
		int ie = -1; // index of the last element matching any of the parameter values.

        for ( int i = b; i < Num; i++ )
        {
            for ( int j = 0; j < e.Length; j++ )
            {
                if ( array[i].Equals( e[j] ) )
                {
                    ie = i;
                    break;
                }
                else if ( ie != -1 )
                {
                    return i;
                }
            }
        }

        return -1;
	}
	/// <summary>
	/// Find the index of the first element that matches any of the specified elements, starting the search at index b.
	/// </summary>
	/// <param name="b">Starting index of the search.</param>
	/// <param name="e">Elements to match against.</param>
	/// <returns>True if an element is found, false otherwise. Its index or -1 is stored in the ref parameter.</returns>
	public bool FindAny		( ref int b, params T[] e )
	{
        for ( int i = b ; i < array.Length ; i++ )
        {
            for (int j = 0; j < e.Length; j++)
            {
                if (array[i].Equals(e[j]))
				{
					b = i;
					return true;
				}
            }
        }

		b = -1;
        return false;
	}
	/// <summary>
	/// Find the index of the first element that matches any of the specified elements, starting the search at index b.
	/// </summary>
	/// <param name="b">Starting index of the search.</param>
	/// <param name="e">Elements to match against.</param>
	/// <returns>An index or -1 if not found.</returns>
	public int  FindAny		( int b, params T[] e )
	{
        for ( int i = b ; i < array.Length ; i++ )
        {
            for (int j = 0; j < e.Length; j++)
            {
                if (array[i].Equals(e[j]))
				{
					return i;
				}
            }
        }

        return -1;		
	}
	/// <summary>
    /// Find the index of any element not one of a set of values, searching from b.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="a">List of search values.</param>
    /// <returns>The index of the first match. -1 if no matches found.</returns>
    public int  FindNot		( int b, params T[] a )
    {
        bool match;

        for ( int i = b ; i < Num ; i++ )
        {
            match = false;
            for (int j = 0; j < a.Length; j++)
            {
                if ( array[i].Equals( a[j] ) ) { match = true; break; }
            }
            if ( ! match ) return i;
        }

        return -1;
    }
    /// <summary>
    /// Get the intersection of an array with this array.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="set">A set of items.</param>
    /// <returns>An array of all items of the given array that are
    /// also members of this array, including duplicates.</returns>
    public Bag<T> Intersection ( Bag<T> set )
    {
        Bag<T> intersection = new Bag<T>();

        for (int i = 0; i < set.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j]))
                {
                    intersection.Add(set.array[i]);
                    break;
                }
            }
        }

        return intersection;
    }

    /// <summary>
    /// Determine if the collection has a member that matches the condition of the
    /// given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="condition">Delegate function.</param>
    /// <returns>True if a match is found, false otherwise.</returns>
    public bool Has(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return true;
        }

        return false;
    }
    /// <summary>
    /// Find the index of the first item matching the condition of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the match or -1.</returns>
    public int FindFirst(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the index of the last item matching the condition of the given delegate.
    /// <para>Teste.</para>
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the matching item or -1 if no match is found.</returns>
    public int FindLast(Func<T, bool> condition)
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i])) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the first item matching the condition of the given delegate, starting
    /// the search from the given index.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function.</param>
    /// <returns>An index or -1 if no match is found.</returns>
    public int FindNext		( int b, Func<T, bool> condition )
    {
        for ( int i = b; i < Num; i++ )
        {
            if ( condition( array[i] ) )
            {
                return i;
            }
        }

        return -1;
    }
    /// <summary>
    /// Find the last item before or at the given index matching the condition
    /// of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>An index or -1 if no match was found.</returns>
    public int FindPrior	( int b, Func<T, bool> condition )
    {
        for (int i = b; i >= 0; i--)
        {
            if ( condition( array[i] ) )
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// Find the first element from the given index that does not match the
    /// condition of the given delegate, but is preceded by such a match.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool FindPast(ref int b, Func<T, bool> condition)
    {
        int m = -1; // index of the last element matching the condition.

        for (int i = b; i < Num; i++)
        {
            if (condition(array[i]))
            {
                m = i;
            }
            else if (m != -1)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Get the first item matching the condition of the given delegate.
    /// <para>Teste.</para>
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A matching item or the default value of the type if no match is found.
    /// The default value is null for reference types and zero for value types.</returns>
    public T GetFirst(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return array[i];
        }
        return default(T);
    }
    /// <summary>
    /// Get the last item matching the condition of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>An item or the default value of the type if no match is found (null for
    /// reference types, zero for value types).</returns>
    public T GetLast(Func<T, bool> condition)
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i])) return array[i];
        }
        return default(T);
    }
    /// <summary>
    /// Get the first item from the given index that matches the condition
    /// of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Index to begin search at.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>An item if found or the default value of the type (zero for value types,
    /// null for reference types). Its index or -1 is stored in the ref parameter.</returns>
    public T GetNext(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i])) { b = i; return array[i]; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get the last item before or at the given index matching the condition
    /// of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>An item or the default value of the type (zero for value types, null for reference types.
    /// Its index or -1 is stored in the ref parameter.</returns>
    public T GetPrior(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i >= 0; i--)
        {
            if (condition(array[i])) { b = i; return array[i]; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get a collection of all items matching the condition of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A collection of items.</returns>
    public Bag<T> GetAll(Func<T, bool> condition)
    {
        Bag<T> log = new Bag<T>();

        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) log.Add(array[i]);
        }
        return log;
    }
    #endregion SEARCH

    #region EDIT
    /// <summary>
    /// Append an item to the end of the array.
    /// </summary>
    /// <param name="e">Appending item.</param>
    public void Add(T e)
    {
        if (Num + 1 > Max) Expand(1 + Buffer);

        array[Num++] = e;
    }
    /// <summary>
    /// Append all elements of an array to the end of this array.
    /// </summary>
    /// <param name="s">Appending set.</param>
    public void Add(Bag<T> a)
    {
        if (Num + a.Length > Max) Expand(a.Length + Buffer);

        for (int i = 0; i < a.Length; i++) array[Num++] = a.array[i];
    }
    /// <summary>
    /// Append an item to the end of the array.
    /// </summary>
    /// <param name="e">Appending item.</param>
    public void Append(T e)
    {
        if (Num + 1 > Max) Expand(1 + Buffer);

        array[Num++] = e;
    }
    /// <summary>
    /// Append all elements of an array to the end of this array.
    /// </summary>
    /// <param name="s">Appending set.</param>
    public void Append(Bag<T> a)
    {
        if (Num + a.Length > Max) Expand(a.Length + Buffer);

        for (int i = 0; i < a.Length; i++) array[Num++] = a.array[i];
    }
    /// <summary>
    /// Remove an item at a given index.
    /// </summary>
    /// <param name="index">Index of item to remove.</param>
    /// <returns>The removed item.</returns>
    public T RemoveAt(int index)
    {
        T temp = array[index];

        // Left shift all items after index.
        for (int i = index; i < Num - 1; i++)
        {
            array[i] = array[i + 1];
        }

        Num--;
        if (Max - Num > Buffer) Reduce();

        return temp;
    }
    /// <summary>
    /// Remove all items of the given value from the array.
    /// </summary>
    /// <param name="e">Value to remove.</param>
    /// <returns>Number of removed items.</returns>
    public int Remove(T e)
    {
        int i = 0, s = 0;

        while (i < Num)
        {
            if (array[i].Equals(e)) s++;
            else array[i - s] = array[i];
            i++;
        }

        Num -= s;

        if (Max - Num > Buffer) Reduce();
        return s;
    }
    /// <summary>
    /// Remove all subarrays matching a given array.
    /// </summary>
    /// <param name="a">Subarray to remove.</param>
    /// <returns>Number of subarrays removed.</returns>
    public int Remove(Bag<T> a)
    {
        return Replace(a, new Bag<T>(0));
    }
    /// <summary>
    /// Right-shift all items from a given index and assign
    /// a new item to that index.
    /// </summary>
    /// <param name="e">New item.</param>
    /// <param name="index">Index of insertion.</param>
    public void Insert(T e, int index)
    {
        if (Num + 1 > Max) Expand(1 + Buffer);

        // Out of bounds response:
        if (index >= Num)			// Append
        {
            array[Num++] = e;
            return;
        }
        else if (index < 0) index = 0;

        // Right shift all from index.
        for (int i = Num; i > index; i--) array[i] = array[i - 1];

        // Insert item.
        array[index] = e;
        Num += 1;
    }
    /// <summary>
    /// Insert the items of an array into this array at a given index.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Insertion array.</param>
    /// <param name="index">Insertion position.</param>
    public void Insert(Bag<T> a, int index)
    {
        if (Num + a.Num > Max) Expand(a.Num + Buffer);

        // Out of bounds response:
        if (index >= Num)			// Append
        {
            for (int i = 0; i < a.Num; i++)
            {
                array[Num++] = a.array[i];
            }
            return;
        }
        else if (index < 0) index = 0;

        // Right shift elements
        for (int i = Num + a.Num - 1; i >= index + a.Num; i--) array[i] = array[i - a.Num];
        Num += a.Num;

        // Write new elements
        for (int i = 0; i < a.Num; i++) array[index++] = a[i];
    }
    /// <summary>
    /// Replace all items of a given value with a new value.
    /// </summary>
    /// <param name="o">Old value.</param>
    /// <param name="n">New value.</param>
    /// <returns>Number of replaced items.</returns>
    public int Replace(T o, T n)
    {
        int c = 0;
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(o))
            {
                array[i] = n;
                c++;
            }
        }

        return c;
    }
    /// <summary>
    /// Replace all subarrays of a given value with a new subarray.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="o">Old subarray.</param>
    /// <param name="n">New subarray.</param>
    /// <returns>Number of replacements made.f</returns>
    public int Replace(Bag<T> o, Bag<T> n)
    {
        Bag<T> t = new Bag<T>();
        int i = 0, b = 0, r = 0;	// r = number of replacements made.
        bool match = false;

        while (b < Num)
        {
            // Find i for o in this from b to Num-1
            for (i = b; i < Num; i++)
            {
                match = true;
                for (int j = 0; j < o.Num; j++)
                {
                    if (i + j >= Num) { match = false; break; }
                    if (!array[i + j].Equals(o.array[j]))
                    { match = false; break; }
                }
                if (match) break;
            }
            // o found at index i
            if (match)
            {
                t.Add(Copy(b, i - b));
                t.Add(n);
                b = i + o.Num;
                r++;
            }
            // o not found.
            else { t.Add(Copy(b, Num - b)); break; }
        }

        array = t.array;
        Num = t.Num;
        Max = t.Max;

        if (Max - Num > Buffer) Reduce();
        return r;
    }
    /// <summary>
    /// Get a subarray of n items from index b.
    /// </summary>
    /// <param name="b">Beginning index of subarray.</param>
    /// <param name="n">Number of items to copy.</param>
    /// <returns>A subarray of this array.</returns>
    public Bag<T> Copy(int b, int n)
    {
        Bag<T> temp = new Bag<T>(n);

        for (int i = 0; i < n && b + i < Num; i++) temp.Add(array[b + i]);

        return temp;
    }

    /// <summary>
    /// Remove all items matching the condition of the given delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>The number of items removed.</returns>
    public int RemoveAll(Func<T, bool> condition)
    {
        int i = 0, s = 0;	// s for shift.

        while (i < Num)
        {
            if (condition(array[i])) s++;
            else array[i - s] = array[i];
            i++;
        }

        Num -= s;

        if (Max - Num > Buffer) Reduce();
        return s;
    }
    /// <summary>
    /// Insert a new item at the first index whose current item matches the
    /// condition of the delegate.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="n">New item.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>The index where the insertion occurred or -1.</returns>
    public int Insert(T n, Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i]))
            {
                if (Num + 1 > Max) Expand(1 + Buffer);

                // Right shift all from index.
                for (int s = Num; s > i; s--) array[s] = array[s - 1];

                // Insert item.
                array[i] = n;
                Num += 1;
                return i;
            }
        }

        return -1;
    }
    /// <summary>
    /// Insert a new item at the first index from a given index whose current
    /// item matches the condition of the given delegate.
    /// </summary>
    /// <param name="b">Index to search from.</param>
    /// <param name="n">Item to insert.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>True if a replacement was made, false otherwise. The insertion index
    /// or -1 is stored in the ref parameter.</returns>
    public bool Insert(T n, ref int b, Func<T, bool> condition)
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i]))
            {
                if (Num + 1 > Max) Expand(1 + Buffer);

                // Right shift all from index.
                for (int s = Num; s > i; s--) array[s] = array[s - 1];

                // Insert item.
                array[i] = n;
                Num += 1;

                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Insert an item at the first index whose item does not match the condition
    /// of the given delegate, but is preceded by one such.
    /// </summary>
    /// <param name="n">Item to insert.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>The index of the insertion or -1.</returns>
    public int InsertPast(T n, Func<T, bool> condition)
    {
        int m = -1; // index of the last element matching the condition.

        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i]))
            {
                m = i;
            }
            else if (m != -1)
            {
                if (Num + 1 > Max) Expand(1 + Buffer);

                // Right shift all from index.
                for (int s = Num; s > i; s--) array[s] = array[s - 1];

                // Insert item.
                array[i] = n;
                Num += 1;

                return i;
            }
        }

        return -1;
    }
    /// <summary>
    /// Insert a new item at the first index from a given index, whose
    /// current item does not match the condition of the delegate, but
    /// is preceded by one such.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Index to search from.</param>
    /// <param name="n">Item to insert.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>True if a match was found and an insertion made, false otherwise. The
    /// indsertion index is stored in the ref parameter.</returns>
    public bool InsertPast(T n, ref int b, Func<T, bool> condition)
    {
        int m = -1; // index of the last element matching the condition.

        for (int i = b; i < Num; i++)
        {
            if (condition(array[i]))
            {
                m = i;
            }
            else if (m != -1)
            {
                if (Num + 1 > Max) Expand(1 + Buffer);

                // Right shift all from index.
                for (int s = Num; s > i; s--) array[s] = array[s - 1];

                // Insert item.
                array[i] = n;
                Num += 1;

                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Replace all items matching the condition of the given delegate
    /// with the specified value.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="newitem">Replacement value.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>The number of replacements made.</returns>
    public int Replace(T newitem, Func<T, bool> condition)
    {
        int c = 0;
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i]))
            {
                array[i] = newitem;
                c++;
            }
        }

        return c;
    }

    /// <summary>
    /// Perform the operation of the given delegate for each and every item.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="action">A Delegate taking an item as parameter and returning void.</param>
    public void OnAll(Action<T> action)
    {
        for (int i = 0; i < Num; i++) action(array[i]);
    }
    #endregion // EDIT

    #region ARRANGE
    /// <summary>
    /// Perform an insertion sort on the array, where items are compared by
    /// the provided callback.
    /// <para>Lambda expression example: Sort( (item1, item2) => { return item1.CompareTo( item2 ); } );</para>
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="comparer">Callback delegate.</param>
    public void Sort(Comparison<T> comparer)
    {
        int i, j;
        int n = Num; if (n < 2) return;
        T a;

        for (j = 1; j < n; j++)		// Pick out each element in turn.
        {
            a = array[j];
            i = j - 1;

            while (i >= 0 && comparer(array[i], a) > 0)	// Look for the place to insert it.
            {
                array[i + 1] = array[i];
                i--;
            }

            array[i + 1] = a;					// Insert it.
        }
    }
    /// <summary>
    /// Quick sort the items within the given index range using the provided comparison callback.
    /// <para>Lambda expression example: QuickSort( 0, 10, (item1, item2) => { return item1.CompareTo( item2 ); } );</para>
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="lo">Lower index of sort region.</param>
    /// <param name="hi">Upper index of sort region.</param>
    /// <param name="comparer">Comparison callback delegate.</param>
    public void QuickSort(int lo, int hi, Comparison<T> comparer)
    {
        if (hi == -1) hi = Num - 1;

        int i = lo, j = hi;
        T h;
        T x = array[(lo + hi) / 2];

        //  partition
        do
        {
            while (comparer(array[i], x) < 0) i++;
            while (comparer(array[j], x) > 0) j--;
            if (i <= j)
            {
                h = array[i]; array[i] = array[j]; array[j] = h;
                i++; j--;
            }
        } while (i <= j);

        //  recursion
        if (lo < j) QuickSort(lo, j, comparer);
        if (i < hi) QuickSort(i, hi, comparer);
    }
    /// <summary>
    /// Shift all items a number of steps to the left or right, looping
    /// items to the other end of the array if they end up out of bounds.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="s">Number of steps to shift. Positive s shifts towards higher
    /// indexes. Negative s shifts towards lower indexes.</param>
    public void ShiftLoop(int s)
    {
        if (Num <= 0) return; s = -s;

        int di = ((s < 0) ? (s - Num + 1) % (Num) + Num - 1 : ((s > Num - 1) ? (s) % (Num) : s));

        int i;

        T[] temp = new T[Max];

        for (i = 0; i < Num - di; i++) temp[i] = array[di + i];
        for (i = 0; i < di; i++) temp[Num - di + i] = array[i];

        array = temp;
    }
    /// <summary>
    /// Exchange the positions of two elements.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
    public void Swap(int i, int j)
    {
        if (i == j) return;

        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    /// <summary>
    /// Invert item order.
    /// </summary>
    public void Invert()
    {
        if (Num <= 1) return;

        int n = (int)(Num * 0.5f);
        int u = Num - 1;

        for (int i = 0; i < n; i++) Swap(i, u - i);
    }
    #endregion // ARRANGE

    #region INTERFACES
    /// <summary>
    /// Two bags are equal if they have the same number of elements and
    /// all elements exist in both sets, disregarding order.
    /// </summary>
    /// <param name="o">Comparison list.</param>
    /// <returns>True if item count and all items are equal. False otherwise.</returns>
    public override bool Equals(object o)
    {
        if (o is Bag<T>) return this == (Bag<T>)o;
        else return false;
    }
    public override int GetHashCode()
    {
        return 0;
    }
    void IDisposable.Dispose() { }
    #endregion

    #region ENUMERATION
    public IEnumerator<T> GetEnumerator()
    {
        enumerationPosition = -1;
        return (IEnumerator<T>)this;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public bool MoveNext()
    {
        if (++enumerationPosition >= Length) { Reset(); return false; }
        else return true;
    }
    public void Reset()
    {
        enumerationPosition = -1;
    }
    public T Current
    {
        get { return array[enumerationPosition]; }
    }
    object System.Collections.IEnumerator.Current
    {
        get { return Current; }
    }
    #endregion // ENUMERATION
    #region SERIALIZATION
    protected Bag(SerializationInfo s, StreamingContext c)
    {
        this.buffer = s.GetInt32("buffer");
        this.Max = s.GetInt32("max");
        this.Num = s.GetInt32("num");
        this.dval = (T)s.GetValue("dval", typeof(T));

        this.array = (T[])s.GetValue("array", typeof(T[]));
    }
    protected virtual void GetObjectData(SerializationInfo s, StreamingContext c)
    {
        s.AddValue("buffer", buffer);
        s.AddValue("max", Max);
        s.AddValue("num", Num);
        s.AddValue("dval", dval);

        s.AddValue("array", array);
    }
    void ISerializable.GetObjectData(SerializationInfo s, StreamingContext c)
    {
        this.GetObjectData(s, c);
    }
    #endregion
}


/// <summary>
/// A set is a collection of unique elements, such that no two elements may have the same value.
/// <para>Two sets are equal if every value of one is also a member of the second, disregarding order.</para>
/// </summary>
/// <typeparam name="T">Data type of the elements of the set.</typeparam>
[Serializable]
public class Set<T> : IEnumerator<T>, IEnumerable<T>, ISerializable
{
    #region DATA
    /// <summary>Buffer size.</summary>
    protected int buffer;
    /// <summary>Array of elements.</summary>
    protected T[] array;					// C-style array.
    /// <summary>Number of allocated elements.</summary>
    protected int Max;
    /// <summary>Number of used elements.</summary>
    protected int Num;
    /// <summary>Default element value.</summary>
    protected T dval = default(T);
    /// <summary>Random number generator.</summary>
    protected Random random = new Random();

    protected int enumerationPosition = -1;
    #endregion

    #region CONSTRUCTION (Tested)
    /// <summary>
    /// Default constructor with initial size 30 and buffer count 30.
    /// </summary>
    public Set()
    {
        array = null; buffer = 30;
        Num = 0; Max = 30;

        InitArray();
    }
    /// <summary>
    /// Constructor for initial capacity. Buffer count 30.
    /// </summary>
    /// <param name="max">Initial array capacity.</param>
    public Set(int max)
    {
        array = null; buffer = 30;
        Num = 0; Max = max;

        InitArray();
    }
    /// <summary>
    /// Copy the items of the array, excluding duplicate values.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Source array.</param>
    public Set(T[] a)
    {
        buffer = 30;
        Max = (a == null) ? 0 : a.Length; Num = 0;

        InitArray();
        for (int i = 0; i < Max; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (a[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else array[Num++] = a[i];
        }
    }
    /// <summary>
    /// Copy constructor.
    /// </summary>
    /// <param name="a">Source list.</param>
    public Set(Set<T> a)
    {
        buffer = 30;
        Num = (a == null) ? 0 : a.Length; Max = Num;

        InitArray();
        for (int i = 0; i < Num; i++) array[i] = a.array[i];
    }
    /// <summary>
    /// Destructor.
    /// </summary>
    ~Set()
    {
        Delete();
    }
    #endregion		// CONSTRUCTION

    #region MEMORY OPERATIONS (Tested)
    /// <summary>
    /// Allocate memory buffer.
    /// </summary>
    protected void InitArray()
    {
        array = new T[Max];
    }
    /// <summary>
    /// Deallocate memory.
    /// </summary>
    protected void Delete()
    {
        array = null;
        Num = 0;
        Max = 0;
    }
    /// <summary>
    /// Empty the array.
    /// </summary>
    public void    Clear()
    {
        Delete(); Num = 0; Max = buffer;
        InitArray();
    }
    /// <summary>
    /// Expand and reallocate.
    /// </summary>
    /// <param name="size">Expansion count.</param>
    protected void Expand(int size)
    {
        if (size < 1) return;
        T[] temp = new T[Max + size];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max += size;
    }
    /// <summary>
    /// Free unused memory.
    /// </summary>
    protected void Reduce()
    {
        T[] temp = new T[Num];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max = Num;
    }
    #endregion

    #region PROPERTIES (Tested)
    /// <summary>
    /// Get the item at an index, or set the item at an index if its not already a member.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="i">Index of an item.</param>
    /// <returns>The item at the index.</returns>
    public virtual T this[int i]
    {
        get
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();
            return array[i];
        }
        set
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();

            for (int j = 0; j < Num; j++)
            {
                if (array[j].Equals(value)) return;
            }

            array[i] = value;
        }
    }
    /// <summary>Get or set the memory buffer.</summary>
    public int Buffer { set { buffer = value; } get { return buffer; } }
    /// <summary>Get a copy of the internal System.Array.</summary>
    public T[] Array
    {
        get
        {
            T[] clone = new T[Num];
            for (int i = 0; i < Num; i++) clone[i] = array[i];
            return clone;
        }
    }

    /// <summary>Get the number of items added to the collection.</summary>
    public int Length { get { return Num; } }
    /// <summary>Get the size of the collection including empty elements.</summary>
    public int Size { get { return Max; } }
    public T DefaultValue { set { dval = value; } get { return dval; } }
    #endregion

    #region OPERATORS (Tested)
    /// <summary>
    /// Equality operator. Test if two sets have identical contents,
    /// disregarding order.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if every member of one set is also a member of the other set.</returns>
    public static bool operator ==(Set<T> a, Set<T> b)
    {
        if (object.ReferenceEquals(a, b)) return true;
        else if (object.ReferenceEquals(a, null)) return false;
        else if (object.ReferenceEquals(b, null)) return false;
        else if (a.Num != b.Num) return false;

        for (int i = 0; i < a.Num; i++)
        {
            bool exists = false;
            for (int j = 0; j < b.Num; j++)
            {
                if (a.array[i].Equals(b.array[j]))
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) return false;
        }

        return true;
    }
    /// <summary>
    /// Inequality operator. Test if two sets are not identical.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if any value is in one set but not the other.</returns>
    public static bool operator !=(Set<T> a, Set<T> b)
    {
        if (ReferenceEquals(a, b)) return false;
        else if (ReferenceEquals(a, null)) return true;
        else if (ReferenceEquals(b, null)) return true;
        else if (a.Num != b.Num) return true;

        for (int i = 0; i < a.Num; i++)
        {
            bool exists = false;
            for (int j = 0; j < b.Num; j++)
            {
                if (a.array[i].Equals(b.array[j]))
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) return true;
        }

        return false;
    }
    /// <summary>
    /// Union operator. Appends one item to the end of the set if its
    /// not already a member of the set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to append to.</param>
    /// <param name="b">Item to append.</param>
    /// <returns>The union of the set and the item.</returns>
    public static Set<T> operator +(Set<T> a, T b)
    {
        Set<T> union = new Set<T>(a);
        union.Add(b);
        return union;
    }
    /// <summary>
    /// Union operator. Appends any element of the second operand to the end
    /// of the first, if its not already a member of that set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">First source set.</param>
    /// <param name="b">Set to append.</param>
    /// <returns>The union of the two sets.</returns>
    public static Set<T> operator +(Set<T> a, Set<T> b)
    {
        Set<T> union = new Set<T>(a);
        union.Add(b);
        return union;
    }
    /// <summary>
    /// Set difference operator.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to reduce.</param>
    /// <param name="b">Element to remove.</param>
    /// <returns>A set of all elements of a that are not equal to b.</returns>
    public static Set<T> operator -(Set<T> a, T b)
    {
        Set<T> difference = new Set<T>(a.Num);
        int num = 0;

        for (int i = 0; i < a.Num; i++)
        {
            if (a.array[i].Equals(b)) continue;
            else difference.array[num++] = a.array[i];
        }

        difference.Num = num;
        return difference;
    }
    /// <summary>
    /// Set difference operator.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to reduce.</param>
    /// <param name="b">Set to remove.</param>
    /// <returns>All elements of a that are not in b.</returns>
    public static Set<T> operator -(Set<T> a, Set<T> b)
    {
        Set<T> difference = new Set<T>(a.Num);
        int num = 0;

        for (int i = 0; i < a.Length; i++)
        {
            bool match = false;
            for (int j = 0; j < b.Num; j++)
            {
                if (a.array[i].Equals(b.array[j]))
                {
                    match = true;
                    break;
                }
            }

            if (match) continue;
            else difference.array[num++] = a.array[i];
        }

        difference.Num = num;
        return difference;
    }
    #endregion	// OPERATORS

    #region SEARCH
    /// <summary>
    /// Determine if a value is in the list.
    /// </summary>
    /// <param name="e">Search value.</param>
    /// <returns>True if found. False otherwise.</returns>
    public bool Has(T e)
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if a given array is a subarray of this array.
    /// </summary>
    /// <param name="a">Array to check.</param>
    /// <returns>True if all items of a appear in an unbroken sequence in this array.</returns>
    public bool Has(Set<T> a)
    {
        for (int i = 0; i <= Num - a.Num; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Num; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if any one item of a set appears in this array.
    /// </summary>
    /// <param name="set">Items to find.</param>
    /// <returns>True if at least one item appear this array.</returns>
    public bool HasAny(params T[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set[i].Equals(array[j])) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if the set of items of an array intersects with the items of this array, disregarding order.
    /// </summary>
    /// <param name="set">Set of items.</param>
    /// <returns>True if at least one item is a member of both arrays.</returns>
    public bool HasAny(Set<T> set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j])) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// </summary>
    /// <param name="set">Items to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool HasAll(params T[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (set[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// </summary>
    /// <param name="set">Item to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool HasAll(Set<T> set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Find the index of a member.
    /// </summary>
    /// <param name="e">Searched item.</param>
    /// <returns>An index or -1 if the item is not in the array.</returns>
    public int Find(T e)
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the index of a member starting the search at index b.
    /// </summary>
    /// <param name="b">Start index of the search.</param>
    /// <param name="e">Item to find.</param>
    /// <returns>True if the item was found (its index output to b), false otherwise.</returns>
    public bool FindNext(ref int b, T e)
    {
        for (int i = b; i < Num; i++)
        {
            if (array[i].Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the start index of a subarray starting the search at index b.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="a">Array to find.</param>
    /// <returns>True if a subarray was found (its index output to b), false otherwise.</returns>
    public bool FindNext(ref int b, Set<T> a)
    {
        for (int i = b; i <= Num - a.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Length; j++)
            {
                if (!a.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match) { b = i; return true; }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the index of a member, starting the search at b and going backwards (towards lower
    /// indeces).
    /// </summary>
    /// <param name="b">Start index of search</param>
    /// <param name="e">Searched item.</param>
    /// <returns>True if the item was found, false otherwise. Its index or -1 is
    /// stored in the ref parameter.</returns>
    public bool FindPrior(ref int b, T e)
    {
        for (int i = b; i >= 0; i--)
        {
            if (array[i].Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the start index of a sequence ending before or at the 
    /// given index, searching towards lower indexes.
    /// </summary>
    /// <param name="b">The search starts at b+1-log.Length.</param>
    /// <param name="log">Sequence of items to find.</param>
    /// <returns>True if the sequence was found, false otherwise. Its start index
    /// is stored in the ref parameter.</returns>
    public bool FindPrior(ref int b, Set<T> log)
    {
        for (int i = b + 1 - log.Num; i >= 0; i--)
        {
            bool match = true;
            for (int j = 0; j < log.Num; j++)
            {
                if (!log.array[j].Equals(array[i + j]))
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the first element from the given index that does not match any of
    /// the given values, but is preceded by one such.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="e">Values find and skip past.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool FindPast(ref int b, params T[] e)
    {
        int ie = -1; // index of the last element matching any of the parameter values.

        for (int i = b; i < Num; i++)
        {
            for (int j = 0; j < e.Length; j++)
            {
                if (array[i].Equals(e[j]))
                {
                    ie = i;
                    break;
                }
                else if (ie != -1)
                {
                    b = i;
                    return true;
                }
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Get the intersection of an array with this array.
    /// </summary>
    /// <param name="set">A set of items.</param>
    /// <returns>An array of all items of the given array that are also members of this array.</returns>
    public Set<T> Intersection(Set<T> set)
    {
        Set<T> intersection = new Set<T>();

        for (int i = 0; i < set.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (set.array[i].Equals(array[j]))
                {
                    intersection.Add(set.array[i]);
                    break;
                }
            }
        }

        return intersection;
    }

    /// <summary>
    /// Determine if the collection has a member that matches the condition of the
    /// given delegate.
    /// </summary>
    /// <param name="condition">Delegate function.</param>
    /// <returns>True if a match is found, false otherwise.</returns>
    public bool Has(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return true;
        }

        return false;
    }
    /// <summary>
    /// Find the index of the first item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the match or -1.</returns>
    public int FindFirst(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the index of the last item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the matching item or -1 if no match is found.</returns>
    public int FindLast(Func<T, bool> condition)
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i])) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the first item matching the condition of the given delegate, starting
    /// the search from the given index.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function.</param>
    /// <returns>True if a match is found, false otherwise. The index or -1 is
    /// stored in the ref parameter.</returns>
    public bool FindNext(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i]))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the last item before or at the given index matching the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>True if a match was found, otherwise false. Its index or -1 is stored in the
    /// ref parameter.</returns>
    public bool FindPrior(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i >= 0; i--)
        {
            if (condition(array[i]))
            {
                b = i;
                return true;
            }
        }
        b = -1;
        return false;
    }
    /// <summary>
    /// Find the first element from the given index that does not match the
    /// condition of the given delegate, but is preceded by such a match.
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool FindPast(ref int b, Func<T, bool> condition)
    {
        int m = -1; // index of the last element matching the condition.

        for (int i = b; i < Num; i++)
        {
            if (condition(array[i]))
            {
                m = i;
            }
            else if (m != -1)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Get the first item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A matching item or the default value of the type if no match is found.
    /// The default value is null for reference types and zero for value types.</returns>
    public T GetFirst(Func<T, bool> condition)
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) return array[i];
        }
        return default(T);
    }
    /// <summary>
    /// Get the last item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>An item or the default value of the type if no match is found (null for
    /// reference types, zero for value types).</returns>
    public T GetLast(Func<T, bool> condition)
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i])) return array[i];
        }
        return default(T);
    }
    /// <summary>
    /// Get the first item from the given index that matches the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Index to begin search at.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>An item if found or the default value of the type (zero for value types,
    /// null for reference types). Its index or -1 is stored in the ref parameter.</returns>
    public T GetNext(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i])) { b = i; return array[i]; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get the last item before or at the given index matching the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>An item or the default value of the type (zero for value types, null for reference types.
    /// Its index or -1 is stored in the ref parameter.</returns>
    public T GetPrior(ref int b, Func<T, bool> condition)
    {
        for (int i = b; i >= 0; i--)
        {
            if (condition(array[i])) { b = i; return array[i]; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get a collection of all items matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A collection of items.</returns>
    public Set<T> GetAll(Func<T, bool> condition)
    {
        Set<T> set = new Set<T>();

        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i])) set.Add(array[i]);
        }
        return set;
    }
    #endregion SEARCH

    #region EDIT
    /// <summary>
    /// Append an item to the set if its not already a member of the set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">Appending item.</param>
    /// <returns>True if appended, false otherwise.</returns>
    public bool Add(T e)
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return false;
        }

        if (Num + 1 > Max) Expand(1 + Buffer);

        array[Num++] = e;
        return true;
    }
    /// <summary>
    /// Append all elements of a set to this set, excluding elements
    /// that are already members of this set. This set becomes the
    /// union of the two sets.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="s">Appending set.</param>
    /// <returns>True if all members were added, false otherwise.</returns>
    public bool Add(Set<T> s)
    {
        bool itemexcluded = false;

        // Iterate through the appending items:
        for (int i = 0; i < s.Length; i++)
        {
            // Check if the appending item is already in this set.
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (s[i].Equals(array[j]))
                {
                    exists = true;
                    break;
                }
            }

            // Add the appending item if it was not in this set.
            if (exists)
            {
                itemexcluded = true;
                continue;
            }
            else
            {
                if (Num + 1 > Max) Expand(1 + Buffer);
                array[Num++] = s[i];
            }
        }

        return !itemexcluded;
    }
    /// <summary>
    /// Remove an item at a given index.
    /// </summary>
    /// <param name="index">Index of item to remove.</param>
    /// <returns>The removed item.</returns>
    public T RemoveAt(int index)
    {
        T temp = array[index];

        // Left shift all items after index.
        for (int i = index; i < Num - 1; i++)
        {
            array[i] = array[i + 1];
        }

        Num--;
        if (Max - Num > Buffer) Reduce();

        return temp;
    }
    /// <summary>
    /// Remove the item of the given value from the array.
    /// </summary>
    /// <param name="e">Value to remove.</param>
    /// <returns>True if the item was found, false otherwise.</returns>
    public bool Remove(T e)
    {
        int i = 0, s = 0;

        while (i < Num)
        {
            if (array[i].Equals(e)) s++;
            else array[i - s] = array[i];
            i++;
        }

        Num -= s;

        if (Max - Num > Buffer) Reduce();

        if (s == 0) return false;
        else return true;
    }
	/// <summary>
	/// Swap a member with the member of the next lower index.
	/// </summary>
	/// <param name="e">Member to left-shift.</param>
	public void ShiftLeft ( T e )
	{
		int i = Find( e );
		if ( i > 0 )
		{
			T temp = array[i-1];
			array[i-1] = array[i];
			array[i] = temp;
		}
	}
	/// <summary>
	/// Swap a member with the member of the next higher index.
	/// </summary>
	/// <param name="e">Member to right-shift.</param>
	public void ShiftRight ( T e )
	{
		int i = Find( e );
		if ( i < Num - 1 )
		{
			T temp = array[i+1];
			array[i+1] = array[i];
			array[i] = temp;
		}
	}
	/// <summary>
    /// Exchange the positions of two elements identified by index.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
    public void Swap(int i, int j)
    {
        if (i == j) return;

        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    /// <summary>
    /// Exchange the positions of two elements.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
	public void Swap( T a, T b )
	{
		int ia = -1;

		for ( int i = 0 ; i < Num ; i++ )
		{
			if ( ia == -1 && array[i].Equals( a ) ) ia = i;
			else if ( ia != -1 && array[i].Equals( b ) )
			{
				T temp = array[ia];
				array[ia] = array[i];
				array[i] = temp;
				return;
			}
		}
	}
	
	#endregion // EDIT

    #region INTERFACES
    /// <summary>
    /// Two sets are equal if they have the same number of elements and
    /// all elements exist in both sets, disregarding order.
    /// </summary>
    /// <param name="o">Comparison list.</param>
    /// <returns>True if item count and all items are equal. False otherwise.</returns>
    public override bool Equals(object o)
    {
        if (o is Set<T>) return this == (Set<T>)o;
        else return false;
    }
    public override int GetHashCode()
    {
        return 0;
    }
    public IEnumerator<T> GetEnumerator()
    {
        enumerationPosition = -1;
        return (IEnumerator<T>)this;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public bool MoveNext()
    {
        if (++enumerationPosition >= Length) { Reset(); return false; }
        else return true;
    }
    public void Reset()
    {
        enumerationPosition = -1;
    }
    void IDisposable.Dispose() { }
    public T Current
    {
        get { return array[enumerationPosition]; }
    }
    object System.Collections.IEnumerator.Current
    {
        get { return Current; }
    }
    #endregion
    #region SERIALIZATION
    protected Set(SerializationInfo s, StreamingContext c)
    {
        this.buffer = s.GetInt32("buffer");
        this.Max = s.GetInt32("max");
        this.Num = s.GetInt32("num");
        this.dval = (T)s.GetValue("dval", typeof(T));

        this.array = (T[])s.GetValue("array", typeof(T[]));
    }
    protected virtual void GetObjectData(SerializationInfo s, StreamingContext c)
    {
        s.AddValue("buffer", buffer);
        s.AddValue("max", Max);
        s.AddValue("num", Num);
        s.AddValue("dval", dval);

        s.AddValue("array", array);
    }
    void ISerializable.GetObjectData(SerializationInfo s, StreamingContext c)
    {
        this.GetObjectData(s, c);
    }
    #endregion

}

/// <summary>
/// A collection of indexed elements, where values may occur more than once.
/// Some methods operate on the collection as a list or tuple, others as a bag (multiset).
/// Some operations may treat the collection as a loop if the LOOP flag is on.
/// </summary>
/// <typeparam name="T">Specifies the data type of the elements of the collection.</typeparam>
[Serializable]
public class Loop<T> : IEnumerator<T>, IEnumerable<T>, ISerializable
{
    #region DATA
    /// <summary>Buffer size.</summary>
    protected int buffer;
    /// <summary>Mode flags.</summary>
    protected Flag flags;
    /// <summary>Array of elements.</summary>
    protected T[] array;					// C-style array.
    /// <summary>Number of allocated elements.</summary>
    protected int Max;
    /// <summary>Number of used elements.</summary>
    protected int Num;
    /// <summary>Default element value.</summary>
    protected T dval = default(T);
    /// <summary>Random number generator.</summary>
    protected Random random = new Random();

    protected int enumerationPosition = -1;
    #endregion

    #region TYPES
    [Flags]
    public enum Flag : uint { INSERT = 1u, LOOP = 2u, SET = 4u }
    #endregion

    #region CONSTRUCTION (Tested)
    /// <summary>
    /// Default constructor with initial size 30 and buffer count 30.
    /// </summary>
    public Loop()
    {
        array = null; buffer = 30;
        Num = 0; Max = 30; flags = Flag.INSERT | Flag.LOOP;

        InitArray();
    }
    /// <summary>
    /// Constructor for initial capacity. Buffer count 30.
    /// </summary>
    /// <param name="max">Initial array capacity.</param>
    public Loop(int max)
    {
        array = null; buffer = 30;
        Num = 0; Max = max; flags = Flag.INSERT | Flag.LOOP;

        InitArray();
    }
    /// <summary>
    /// Initializing constructor. Initial size from parameter array. Buffer count 30.
    /// </summary>
    /// <param name="a">Source array.</param>
    public Loop(T[] a)
    {
        buffer = 30;
        Num = (a == null) ? 0 : a.Length; Max = Num; flags = Flag.INSERT | Flag.LOOP;

        InitArray();

        for (int i = 0; i < Num; i++) array[i] = a[i];
    }
    /// <summary>
    /// Copy constructor.
    /// </summary>
    /// <param name="a">Source list.</param>
    public Loop(Loop<T> a)
    {
        buffer = 30;
        Num = (a == null) ? 0 : a.Length; Max = Num; flags = a.flags;

        InitArray();
        for (int i = 0; i < Num; i++) array[i] = a.array[i];
    }
    /// <summary>
    /// Destructor.
    /// </summary>
    ~Loop()
    {
        Delete();
    }
    #endregion		// CONSTRUCTION

    #region SERIALIZATION
    protected Loop(SerializationInfo s, StreamingContext c)
    {
        this.buffer = s.GetInt32("buffer");
        this.flags = (Flag)s.GetUInt32("flags");
        this.Max = s.GetInt32("max");
        this.Num = s.GetInt32("num");
        this.dval = (T)s.GetValue("dval", typeof(T));

        this.array = (T[])s.GetValue("array", typeof(T[]));
    }
    protected virtual void GetObjectData(SerializationInfo s, StreamingContext c)
    {
        s.AddValue("buffer", buffer);
        s.AddValue("flags", flags);
        s.AddValue("max", Max);
        s.AddValue("num", Num);
        s.AddValue("dval", dval);

        s.AddValue("array", array);
    }
    void ISerializable.GetObjectData(SerializationInfo s, StreamingContext c)
    {
        this.GetObjectData(s, c);
    }
    #endregion

    #region MEMORY OPERATIONS (Tested)
    /// <summary>
    /// Allocate memory buffer.
    /// </summary>
    protected void InitArray()								// DEBUGGED
    {
        array = new T[Max];
    }
    /// <summary>
    /// Deallocate memory.
    /// </summary>
    protected void Delete()								// DEBUGGED
    {
        array = null;
        Num = 0;
        Max = 0;
    }
    /// <summary>
    /// Empty the array.
    /// </summary>
    protected void Clear()								// DEBUGGED
    {
        Delete(); Num = 0; Max = buffer;
        InitArray();
    }
    /// <summary>
    /// Expand and reallocate.
    /// </summary>
    /// <param name="size">Expansion count.</param>
    protected void Expand(int size)					// DEBUGGED
    {
        if (size < 1) return;
        T[] temp = new T[Max + size];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max += size;
    }
    /// <summary>
    /// Free unused memory.
    /// </summary>
    protected void Reduce()								// DEBUGGED
    {
        T[] temp = new T[Num];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max = Num;
    }
    #endregion

    #region INDEX OPERATIONS
    // Indexer
    public T this[int i]
    {
        get
        {
            if (Num == 0) throw new ERROR_Field("Field<T>: Indexer get - array is empty.");
            if (Mode(Flag.LOOP)) return array[(i < 0) ? (i - Num + 1) % (Num) + Num - 1 :
                                               (i > Num - 1) ? (i) % (Num) : i];
            if (0 <= i && i < Num) return array[i];

            throw new ERROR_Field("Field<T>: Indexer get - index out of bounds.");
        }
        set
        {
            if (Num == 0) throw new ERROR_Field("Field<T>: Indexer set - array is empty.");
            if (Mode(Flag.LOOP)) array[(i < 0) ? (i - Num + 1) % (Num) + Num - 1 :
                                        (i > Num - 1) ? (i) % (Num) : i] = value;
            else if (0 <= i && i < Num) array[i] = value;

            else throw new ERROR_Field("Field<T>: Indexer set - index out of bounds.");
        }
    }
    // Index validation
    public int Test(int i)
    {
        if (Num == 0) return -1;
        if (Mode(Flag.LOOP)) return ((i < 0) ? (i - Num + 1) % (Num) + Num - 1 : ((i > Num - 1) ? (i) % (Num) : i));
        if (0 <= i && i < Num) return i;

        return -1;
    }
    // Loop index into bounds.
    public int Loopi(int i)							// DEBUGGED
    {
        if (Num == 0) return -1;
        else return ((i < 0) ? (i - Num - 1) % (Num) + Num - 1 : ((i > Num - 1) ? (i) % (Num) : i));
    }
    /// <summary>
    /// Get a random index in the interval between l and h, inclusive.
    /// </summary>
    /// <param name="l">Low limit.</param>
    /// <param name="h">High limit.</param>
    /// <returns>An integer</returns>
    public int Randi(int l, int h)					// WIP
    {
        return random.Next(l, h - 1);
    }
    #endregion

    #region PROPERTIES
    /// <summary>Get or set the memory buffer.</summary>
    public int Buffer { set { buffer = value; } get { return buffer; } }
    public Flag Flags { set { flags = value; } get { return flags; } }
    /// <summary>Get the internal System.Array.</summary>
    public T[] Array
    {
        get
        {
            T[] clone = new T[Num];
            for (int i = 0; i < Num; i++) clone[i] = array[i];
            return clone;
        }
    }

    /// <summary>Get the number of items added to the collection.</summary>
    public int Length { get { return Num; } }
    /// <summary>Get the size of the collection including empty elements.</summary>
    public int Size { get { return Max; } }
    public T DefaultValue { set { dval = value; } get { return dval; } }
    #endregion

    #region MODE OPERATIONS (Tested)
    public void Enable(Flag mode) { flags = flags | mode; }
    public void Disable(Flag mode) { flags = flags & ~mode; }
    /// <summary>
    /// Test if all the given flags are set.
    /// </summary>
    /// <param name="mode">Flags to test.</param>
    /// <returns>True if all flags are set, false otherwise.</returns>
    public bool Mode(Flag mode)
    {
        if ((flags & mode) == mode) return true;
        else return false;
    }
    #endregion

    #region OPERATORS
    /// <summary>
    /// Test OK. Equality operator. Test if two sets have identical contents.
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if both sets contain the same values in the same order and
    /// the number of values are identical.</returns>
    public static bool operator ==(Loop<T> a, Loop<T> b)
    {
        if (ReferenceEquals(a, b)) return true;
        else if (ReferenceEquals(a, null)) return false;
        else if (ReferenceEquals(b, null)) return false;
        else if (a.Num != b.Num) return false;

        for (int i = 0; i < a.Num; i++)
        {
            if (!a[i].Equals(b[i])) return false;
        }

        return true;
    }
    /// <summary>
    /// Inequality operator. Test if two sets are not identical.
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if any value or the number of values or their order
    /// differ.</returns>
    public static bool operator !=(Loop<T> a, Loop<T> b)
    {
        if (ReferenceEquals(a, b)) return false;
        else if (ReferenceEquals(a, null)) return true;
        else if (ReferenceEquals(b, null)) return true;
        else if (a.Num != b.Num) return true;

        for (int i = 0; i < a.Num; i++)
        {
            if (!a[i].Equals(b[i])) return true;
        }

        return false;
    }
    /// <summary>
    /// Concatenation operator. Appends one item to the end of the set.
    /// </summary>
    /// <param name="a">Set to append to.</param>
    /// <param name="b">Item to append.</param>
    /// <returns>A joined list.</returns>
    public static Loop<T> operator +(Loop<T> a, T b)
    {
        Loop<T> ab = new Loop<T>(a);
        ab.Add(b);
        return ab;
    }
    /// <summary>
    /// Concatenation operator. Appends the second operand to the end of the first.
    /// </summary>
    /// <param name="a">First source list.</param>
    /// <param name="b">List to append.</param>
    /// <returns>One joined list.</returns>
    public static Loop<T> operator +(Loop<T> a, Loop<T> b)
    {
        Loop<T> ab = new Loop<T>(a);
        ab.Add(b);
        return ab;
    }
    #endregion	// OPERATORS

    #region SEARCH OPERATIONS
    /// <summary>
    /// Count the number of occurences of a value.
    /// <para>(Test OK)</para>
    /// </summary>
    /// <param name="e">Item to count.</param>
    /// <returns>Number of matches.</returns>
    public int Count(T e)
    {
        int count = 0;
        for (int i = 0; i < Num; i++) if (array[i].Equals(e)) count++;
        return count;
    }
    /// <summary>
    /// Count elements in range. (Test OK)
    /// <para>Search through 'n' number of items starting at index 'b', and compare each
    /// item with the value specified in 'e'. If 'n' is negative, the search goes backwards
    /// (towards lower indexes).</para>
    /// <para>If 'n' is 0 it defaults to a forward search of the entire array (for a
    /// looping array) or to the end of the array (for a non-looping array).
    /// (NOTE: to do a backward search to the beginning of the array just give n the
    /// value of -(b+1).)</para>
    /// <para>For non-looping arrays any search with an out-of-bounds 'b' value will
    /// return 0.</para>
    /// </summary>
    /// <param name="e">Value to search for</param>
    /// <param name="b">Begin count from index b.</param>
    /// <param name="n">Search n number of elements.</param>
    /// <returns>The return value is the number of matches found.</returns>
    public int Count(T e, int b, int n)
    {

        int i, count = 0;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (0 != n--)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            if (array[i].Equals(e)) count++;
        }

        return count;
    }
    /// <summary>
    /// Count subarrays in range. Test OK.
    /// <para>Performs a search on the array exactly like Count(T, int, int), except
    /// it looks for matches with all items of an array within the window specified
    /// by 'b' and 'n'. Overlapping matches are ignored.</para>
    /// <para>For non-looping arrays any search with an out-of-bounds 'begin' value
    /// will return 0.</para>
    /// </summary>
    /// <param name="a">List to count.</param>
    /// <param name="b">Count starts at index b.</param>
    /// <param name="n">Defines the end of the window to search as b+n.</param>
    /// <returns>The return value is the number of whole occurencies of the given
    /// array.</returns>
    public int Count(Loop<T> a, int b, int n)
    {
        int count = 0;
        int k, i, num = a.Num;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; i = b - num + 1; }
        else { fwd = true; i = b; }
        if (n == 0 || n > Num) { n = Num; }

        if (n < num) return 0;
        if (!(Mode(Flag.LOOP) && n == Num)) n = n - num + 1;

        while (n > 0)
        {
            bool match = true;

            for (int j = 0; j < num; j++)
            {
                if (-1 == (k = Test(i + j))) { return count; }
                if (!array[k].Equals(a.array[j])) { match = false; break; }
            }

            if (match)
            {
                count++;
                if (-1 == (i = (fwd) ? Test(i + num) : Test(i - num))) break;
                n -= num;
            }
            else
            {
                if (-1 == (i = (fwd) ? Test(i + 1) : Test(i - 1))) break;
                n -= 1;
            }
        }

        return count;
    }
    /// <summary>
    /// Determine if a value is in the list.
    /// </summary>
    /// <param name="e">Search value.</param>
    /// <returns>True if found. False otherwise.</returns>
    public bool Contains(T e)
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(e)) return true;
        }

        return false;
    }
    /// <summary>
    /// (Test OK) Find a value in the interval from b to b+n-1.
    /// </summary>
    /// <param name="e">Search value.</param>
    /// <param name="b">Start index.</param>
    /// <param name="n">Search count.</param>
    /// <returns>Returns the index of the first matching item found.
    /// If no match is found the return value is -1.</returns>
    public int Find(int b, int n, T e)
    {
        int i;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (n-- != 0)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            if (array[i].Equals(e)) return i;
        }

        return -1;
    }
    /// <summary>
    /// Find the index of the beginning of the first matching subarray searching from
    /// index b to but not including b+n. (Test OK)
    /// </summary>
    /// <param name="a">Search value.</param>
    /// <param name="b">Start index.</param>
    /// <param name="n">Range of search.</param>
    /// <returns></returns>
    public int Find(int b, int n, Loop<T> a)
    {
        int k, i, num = a.Num;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; i = b - num + 1; }
        else { fwd = true; i = b; }
        if (n == 0 || n > Num) { n = Num; }

        if (n < num) return -1;
        if (!(Mode(Flag.LOOP) && n == Num)) n = n - num + 1;

        while (n != 0)
        {
            bool match = true;

            for (int j = 0; j < num; j++)
            {
                if (-1 == (k = Test(i + j))) { return -1; }
                if (!array[k].Equals(a.array[j])) { match = false; break; }
            }

            if (match) return Test(i);

            if (-1 == (i = (fwd) ? Test(++i) : Test(--i))) break;
            n--;
        }

        return -1;
    }
    /// <summary>
    /// Tested. Find the index of any one of a set of values, searching from b to b+n-1.
    /// </summary>
    /// <param name="a">List of search values.</param>
    /// <param name="b">Start index of search.</param>
    /// <param name="n">Range of search.</param>
    /// <returns>The index of the first match. -1 if no matches found.</returns>
    public int FindAny(int b, int n, params T[] a)
    {
        int i;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (n-- != 0)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            for (int j = 0; j < a.Length; j++)
            {
                if (array[i].Equals(a[j])) return i;
            }
        }

        return -1;
    }
    /// <summary>
    /// Find the index of any one of a set of values, searching from b to b+n-1.
    /// </summary>
    /// <param name="a">List of search values.</param>
    /// <param name="b">Start index of search.</param>
    /// <param name="n">Range of search.</param>
    /// <returns>The index of the first match. -1 if no matches found.</returns>
    public int FindAny(int b, int n, Loop<T> a)
    {
        int i;
        bool fwd;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (n-- != 0)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            for (int j = 0; j < a.Length; j++)
            {
                if (array[i].Equals(a[j])) return i;
            }
        }

        return -1;
    }
    /// <summary>
    /// Find the index of any value not one of a set of values, searching from b to b+n-1.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">List of search values.</param>
    /// <param name="b">Start index of search.</param>
    /// <param name="n">Range of search.</param>
    /// <returns>The index of the first match. -1 if no matches found.</returns>
    public int FindNot(int b, int n, params T[] a)
    {
        int i;
        bool fwd, match;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (n-- != 0)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            match = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (array[i].Equals(a[j])) { match = true; break; }
            }
            if (match) continue;
            else return i;
        }

        return -1;
    }
    /// <summary>
    /// Find the index of any value not one of a set of values, searching from b to b+n-1.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">List of search values.</param>
    /// <param name="b">Start index of search.</param>
    /// <param name="n">Range of search.</param>
    /// <returns>The index of the first match. -1 if no matches found.</returns>
    public int FindNot(int b, int n, Loop<T> a)
    {
        int i;
        bool fwd, match;

        if (n < 0) { n = -n; fwd = false; }
        else { fwd = true; }
        if (n == 0 || n > Num) { n = Num; }

        while (n-- != 0)
        {
            if (-1 == (i = (fwd) ? Test(b++) : Test(b--))) break;
            match = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (array[i].Equals(a[j])) { match = true; break; }
            }
            if (match) continue;
            else return i;
        }

        return -1;
    }
    /// <summary>
    /// Tested. Find the next value that is not equal to any of the given ones,
    /// but that is immediately preceeded by any one such.
    /// </summary>
    /// <param name="b">Index to start the search at.</param>
    /// <param name="n">Number of items to limit the search to.</param>
    /// <param name="a">Values, any of which will separate the search value
    /// from the current value.</param>
    /// <returns>An index or -1 if no match was found.</returns>
    public int FindAfter(int b, int n, params T[] a)
    {
        int i = FindAny(b, n, a);

        if (n != 0) n = n - (i - b);

        return FindNot(i, n, a);
    }
    /// <summary>
    /// Find the next character that is not equal to any of the given ones,
    /// but that is immediately preceeded by any one such.
    /// </summary>
    /// <param name="b">Index to start the search at.</param>
    /// <param name="n">Number of items to limit the search to.</param>
    /// <param name="a">Values, any of which will separate the search value
    /// from the current value.</param>
    /// <returns>An index or -1 if no match was found.</returns>
    public int FindAfter(int b, int n, Loop<T> a)
    {
        int i = FindAny(b, n, a);
        if (n != 0) n = n - (i - b);

        return FindNot(i, n - (i - b), a);
    }
    /// <summary>
    /// Return a random value from the list.
    /// </summary>
    /// <returns>A list value.</returns>
    public T Sample()
    {
        return array[random.Next(0, Num)];
    }
    #endregion	// SEARCHOPS

    #region EDIT OPERATIONS (Tested)
    /// <summary>
    /// Test OK. Append an element to the end of the array.
    /// </summary>
    /// <param name="e">Value to append.</param>
    public void Add(T e)
    {
        if (Num + 1 > Max) Expand(1 + Buffer);

        array[Num++] = e;
    }
    /// <summary>
    /// Append a list to the end of this list. (Test OK)
    /// </summary>
    /// <param name="a">List to append.</param>
    public void Add(Loop<T> a)
    {
        if (Num + a.Length > Max) Expand(a.Length + Buffer);

        for (int i = 0; i < a.Length; i++) array[Num++] = a.array[i];
    }
    /// <summary>
    /// Insert n copies of an item starting at a given index. Test OK.
    /// <para>The items at the start index and following are right shifted,
    /// unless insert mode is disabled in which case they are overwritten.</para>
    /// <para>If the start index is out of bounds the new items are
    /// inserted at the beginning of the array (b less than 0), or appended
    /// to the end (b greater than the upper bound).</para>
    /// </summary>
    /// <param name="e">Item to add.</param>
    /// <param name="b">Start index.</param>
    /// <param name="n">Number of copies.</param>
    public void Add(T e, int b, int n)
    {
        bool fwd = n > 0 ? true : false;
        n = n < 0 ? -n : n;

        if (Num + n > Max) Expand(n + Buffer);

        int j, i = Test(b);

        // Out of bounds response:
        if (b >= Num && i == -1)			// Append
        {
            for (j = 0; j < n; j++) array[Num++] = e;
            return;
        }
        else if (i == -1) i = 0;	// Start at beginning

        if (Mode(Flag.INSERT))	// Insert
        {
            for (j = Num + n - 1; j >= i + n; j--) array[j] = array[j - n];
            for (j = 0; j < n; j++) array[i++] = e;
            Num += n;
        }

        else if (Mode(Flag.LOOP))			// Overwrite looping
        {
            while (0 < n--)
            {
                array[Loopi(i)] = e;
                i = fwd ? i + 1 : i - 1;
            }
        }
        else								// Overwrite.
        {
            while (0 < n-- && 0 <= i)
            {
                array[i] = e; if (i == Num) Num++;
                i = fwd ? i + 1 : i - 1;
            }
        }
    }
    /// <summary>
    /// Test OK. Add a number of copies of another list to this list starting at a
    /// given position.
    /// <para>If the start index is negative insertion starts at 0. If the start
    /// index is greater than the upper bound the lists are appended.</para>
    /// <para>This list grow to accomodate the new values, unless loop mode is
    /// enabled and insert mode disabled. Negative n overwrites to the left.</para>
    /// </summary>
    /// <param name="a">A set to append.</param>
    /// <param name="b">Insertion position.</param>
    /// <param name="n">Number of copies of the appending set.</param>
    public void Add(Loop<T> a, int b, int n)
    {
        if (a.Length <= 0) return;

        bool fwd = n < 0 ? false : true;
        n = n < 0 ? -n : n;
        if (Num + a.Num * n > Max) Expand(a.Num * n + Buffer);

        int i = Test(b);
        int m = a.Num * n, j = 0;

        // Out of bounds response:
        if (b >= Num && i == -1)			// Append
        {
            while (0 < m--)
            {
                array[Num++] = a.array[j++];
                if (j == a.Num) j = 0;
            }
            return;
        }
        else if (i == -1) i = 0;	// Start at beginning

        if (Mode(Flag.INSERT))		// Insert at i
        {
            // Right shift elements
            for (j = Num + m - 1; j >= i + m; j--) array[j] = array[j - m];
            Num += m;

            // Write new elements
            j = 0;
            while (0 < m--)
            {
                array[i++] = a.array[j++];
                if (j == a.Num) j = 0;
            }
        }
        else if (Mode(Flag.LOOP))		// Overwrite looping
        {
            j = fwd ? 0 : a.Num - 1;
            while (0 < m--)
            {
                array[i] = a.array[j];

                if (fwd) { if (++i >= Num) i = 0; if (++j >= a.Num) j = 0; }
                else { if (--i < 0) i = Num - 1; if (--j < 0)		j = a.Num - 1; }
            }
        }
        else								// Overwrite at i
        {
            j = fwd ? 0 : a.Num - 1;
            if (!fwd) if (i + 1 < m) { Shift(0, m - i - 1); i += m - i - 1; }
            while (true)
            {
                array[i] = a.array[j];

                if (--m == 0) return;

                if (fwd) { if (++i >= Num) Num++; if (++j >= a.Num) j = 0; }
                else { if (--i < 0) return; if (--j < 0)		j = a.Num - 1; }
            }
        }
    }
    /// <summary>
    /// Test OK. Insert n copies of an item starting at a given position. If the
    /// position equals the upper bound + 1 the insertion amounts to an
    /// append.
    /// </summary>
    /// <param name="e">Item to insert</param>
    /// <param name="b">Start index.</param>
    /// <param name="n">Number of copies.</param>
    public void Insert(T e, int b, int n)
    {
        bool fwd = n > 0 ? true : false;
        n = n < 0 ? -n : n;

        if (Num + n > Max) Expand(n + Buffer);

        int j, i = Test(b);

        // Out of bounds response:
        if (b == Num && i == -1)			// Append
        {
            for (j = 0; j < n; j++) array[Num++] = e;
            return;
        }
        else if (i == -1) return;

        for (j = Num + n - 1; j >= i + n; j--) array[j] = array[j - n];
        for (j = 0; j < n; j++) array[i++] = e;
        Num += n;
    }
    /// <summary>
    /// Insert n copies of an array beginning at index b. If b is lower than
    /// the lower bound the copies are prepended. If b is higher than the
    /// upper bound the copies are appended.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Lot to insert.</param>
    /// <param name="b">Start index of insertion.</param>
    /// <param name="n">Number of copies of a.</param>
    public void Insert(Loop<T> a, int b, int n)
    {
        n = n < 0 ? -n : n;
        if (Num + a.Num * n > Max) Expand(a.Num * n + Buffer);

        int i = Test(b);
        int m = a.Num * n, j = 0;

        // Out of bounds response:
        if (b >= Num && i == -1)			// Append
        {
            while (0 < m--)
            {
                array[Num++] = a.array[j++];
                if (j == a.Num) j = 0;
            }
            return;
        }
        else if (i == -1) i = 0;	// Start at beginning

        // Right shift elements
        for (j = Num + m - 1; j >= i + m; j--) array[j] = array[j - m];
        Num += m;

        // Write new elements
        j = 0;
        while (0 < m--)
        {
            array[i++] = a.array[j++];
            if (j == a.Num) j = 0;
        }
    }
    /// <summary>
    /// Replace all element of a specified value with another value.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="o">Old value.</param>
    /// <param name="n">New value.</param>
    public void Replace(T o, T n)
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Equals(o)) array[i] = n;
        }
    }
    /// <summary>
    /// Replace all sequences of a given permutaion with another sequence.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="o">Old sequence.</param>
    /// <param name="n">New sequence.</param>
    public void Replace(Loop<T> o, Loop<T> n)
    {
        if (o.Num <= 0) return;
        Loop<T> t = new Loop<T>();
        bool loop = Mode(Flag.LOOP);
        int len = Num;

        // If LOOP, copy first o.Num-1 elements to the end.
        if (loop)
        {
            if (Num + o.Num > Max) Expand(o.Num - 1);
            for (int k = 0; k < o.Num - 1; k++) array[k + Num] = array[k];
            len = Num + o.Num - 1;
        }

        int i = 0, b = 0, r = 0;	// r = number of replacements made.
        bool match = false;

        while (b < Num)
        {
            // Find i for o in this from b to Num-1
            for (i = b; i < Num; i++)
            {
                match = true;
                for (int j = 0; j < o.Num; j++)
                {
                    if (i + j >= len) { match = false; break; }
                    if (!array[i + j].Equals(o.array[j]))
                    { match = false; break; }
                }
                if (match) break;
            }
            // o found at index i
            if (match)
            {
                t.Add(Copy(b, i - b));
                t.Add(n);
                b = i + o.Num;
                r++;
            }
            // o not found.
            else { t.Add(Copy(b, Num - b)); break; }
        }

        // Remove excess elements if a cross boundary replacement was made.

        if (b >= Num)
        {
            //int m = ( n.Num - o.Num ) * r - ( t.Num - Num );
            int m = (o.Num - n.Num) * r - (Num - t.Num);
            for (int h = 0; h < m; h++) t.array[h] = t.array[h + t.Num - m];
            t.Num -= m;
        }

        array = t.array;
        Num = t.Num;
        Max = t.Max;

        if (Max - Num > Buffer) Reduce();
    }
    /// <summary>
    /// Replace all values with a given value.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e"></param>
    public void Fill(T e)
    {
        for (int i = 0; i < Num; i++)
        {
            array[i] = e;
        }
    }
    /// <summary>
    /// Remove n items starting at index b, shifting the remaining items
    /// to fill the gap. Fewer than n items may be removed if n passes
    /// the bounds.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index.</param>
    /// <param name="n">Number of items to remove. Negative n removes |n| items
    /// going towards lower indexes.</param>
    public void Remove(int b, int n)
    {
        if (b == Num && n < 0) { Num += n; return; }
        if (-1 == (b = Test(b))) return;

        if (n > 0)
        {
            int e = b + n;

            if (e >= Num)
            {
                if (Mode(Flag.LOOP)) { e = Test(e); Num = b; Shift(e, -e); }
                else Num = b;
            }
            else Shift(e, -n);
        }

        else if (n < 0)
        {
            int e = b + n;

            if (e < 0)
            {
                if (Mode(Flag.LOOP)) { Num += e; }
                Shift(b, -b);
            }
            else Shift(b, n);
        }

        if (Max - Num > Buffer) Reduce();
    }
    /// <summary>
    /// Remove all instances of a given value from this collection.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">object to remove.</param>
    /// <returns>The number of objects removed.</returns>
    public int Remove(T e)
    {
        int i = 0, s = 0;

        while (i < Num)
        {
            if (array[i].Equals(e)) s++;
            else array[i - s] = array[i];
            i++;
        }

        Num -= s;

        if (Max - Num > Buffer) Reduce();
        return s;
    }
    /// <summary>
    /// Remove occurences of a sublist from the collection.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Sublist to remove.</param>
    public void Remove(Loop<T> a)
    {
        Replace(a, new Loop<T>(0));
    }
    /// <summary>
    /// Get a copy of the sublist of n elements starting at index b. Fewer than n
    /// elements may be returned if b+n is greater than the upper bound, unless
    /// looping is enabled.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index.</param>
    /// <param name="n">Number of elements.</param>
    /// <returns>A sublist of elements b to b+n.</returns>
    public Loop<T> Copy(int b, int n)
    {
        if (-1 == (b = Test(b))) return new Loop<T>(0);
        if (n < 0) return new Loop<T>(0);

        Loop<T> a = new Loop<T>(n);

        while (0 < n--)
        {
            a.array[a.Num++] = this[b++];
            if (Test(b) == -1) break;
        }

        return a;
    }
    /// <summary>
    /// Remove and return a sublist of n elements starting at index b. Fewer than n
    /// elements may be removed if b+n is greater than the upper bound, unless
    /// looping is enabled.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Start index.</param>
    /// <param name="n">Number of elements.</param>
    /// <returns>A sublist of elements b to b+n.</returns>
    public Loop<T> Cut(int b, int n)
    {
        if (-1 == (b = Test(b))) return new Loop<T>(0);
        if (n < 0) return new Loop<T>(0);

        Loop<T> a = new Loop<T>(n);

        int i = b, m = n;
        while (0 < m--)
        {
            a.array[a.Num++] = this[i++];
            if (Test(i) == -1) break;
        }

        Remove(b, n);

        return a;
    }
    #endregion

    #region REARRANGE
    /// <summary>
    /// Move the item at an index and all following items a number of step
    /// positions towards higher indexes, expanding the array so no items are lost.
    /// <para>Negative steps results in a left shift (towards lower indexes).
    /// Items that are shifted past the beginning of the array are lost, unless
    /// LOOP is enabled, in which case they rotate to the end of the array.</para>
    /// <para>Left shifts shrink the list.</para>
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="index">Start index.</param>
    /// <param name="step">Number of positions to shift up or down (if negative).</param>
    public void Shift(int index, int step)
    {
        int i, j, b, p;

        if (-1 == (i = Test(index))) return;

        int num = Num + step; if (num < 0) num = 0;

        Loop<T> temp = new Loop<T>(num); temp.Num = num;

        // Copy items with a shift from j to j+step.
        if (i + step < 0) b = i - (i + step); else b = i;
        for (j = b; j < Num; j++) temp.array[j + step] = array[j];

        // Straight copy unaffected items, if any, at the beginning of the array.
        if (step < 0) p = b + step; else p = b;
        for (j = 0; j < p; j++) temp.array[j] = array[j];

        // Loop around items shifted out-of-bounds, if the array is a looping array.
        if (Mode(Flag.LOOP) && num > 0)
        {
            temp.Enable(Flag.LOOP);
            for (j = 0; j < -(i + step); j++) temp[i + j + step] = array[i + j];
        }

        array = temp.array;
        Num = num;
    }
    /// <summary>
    /// Shift all items a number of steps to the left or right, looping
    /// items to the other end of the array if they end up out of bounds.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="s">Number of steps to shift. Positive s shifts towards higher
    /// indexes. Negative s shifts towards lower indexes.</param>
    public void LoopShift(int s)
    {
        if (Num <= 0) return; s = -s;

        int di = ((s < 0) ? (s - Num + 1) % (Num) + Num - 1 : ((s > Num - 1) ? (s) % (Num) : s));

        int i;

        T[] temp = new T[Max];

        for (i = 0; i < Num - di; i++) temp[i] = array[di + i];
        for (i = 0; i < di; i++) temp[Num - di + i] = array[i];

        array = temp;
    }
    /// <summary>
    /// Exchange the positions of two elements.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
    public void Swap(int i, int j)
    {
        if (i == j) return;

        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    /// <summary>
    /// Invert item order.
    /// </summary>
    public void Invert()
    {
        if (Num <= 1) return;

        int n = (int)(Num * 0.5f);
        int u = Num - 1;

        for (int i = 0; i < n; i++) Swap(i, u - i);
    }
    /// <summary>
    /// Randomly rearrange the order of the list.
    /// </summary>
    public void Shuffle()
    {
        throw new NotImplementedException();
    }
    #endregion	// REARRANGE

    #region INTERFACES
    /// <summary>
    /// Test equality.
    /// </summary>
    /// <param name="o">Comparison list.</param>
    /// <returns>True if item count and all items are equal. False otherwise.</returns>
    public override bool Equals(object o)
    {
        if (o is Loop<T>) return this == (Loop<T>)o;
        else return false;
    }
    public override int GetHashCode() { return 0; }
    public IEnumerator<T> GetEnumerator()
    {
        enumerationPosition = -1;
        return (IEnumerator<T>)this;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public bool MoveNext()
    {
        if (++enumerationPosition >= Length) { Reset(); return false; }
        else return true;
    }
    public void Reset()
    {
        enumerationPosition = -1;
    }
    void IDisposable.Dispose() { }
    public T Current
    {
        get { return array[enumerationPosition]; }
    }
    object System.Collections.IEnumerator.Current
    {
        get { return Current; }
    }
    #endregion
}



[Serializable]
public class Stats<T> : Bag<T> where T : IComparable<T>
{
    #region CONSTRUCTION
    /// <summary>Default constructor.</summary>
    public Stats()
    {

    }

    public Stats(int n)
        : base(n)
    {

    }
    /// <summary>Copy constructor.</summary>
    public Stats(Stats<T> source)
        : base(source)
    {

    }
    /// <summary>
    /// Initialise by array.
    /// </summary>
    /// <param name="array"></param>
    public Stats(T[] array)
        : base(array)
    {

    }
    #endregion

    // Serialization:
    protected Stats(SerializationInfo s, StreamingContext c)
        : base(s, c)
    {

    }
    protected override void GetObjectData(SerializationInfo s, StreamingContext c)
    {
        base.GetObjectData(s, c);
    }

    #region SORT
    public void Sort()										// DEBUGGED
    {
        if (Num > 50) QuickSort(0, -1);
        else if (Num > 7) ShellSort(0);
        else InsertSort();
    }
    /// <summary>
    /// Sorts the array in ascending order by straight insertion.
    /// Optimal for arrays of length 20 or less.
    /// </summary>
    public void InsertSort()										// DEBUGGED
    {
        int i, j;
        int n = Num; if (n < 2) return;
        T a;

        for (j = 1; j < n; j++)		// Pick out each element in turn.
        {
            a = array[j];
            i = j - 1;

            while (i >= 0 && array[i].CompareTo(a) > 0)	// Look for the place to insert it.
            {
                array[i + 1] = array[i];
                i--;
            }

            array[i + 1] = a;					// Insert it.
        }
    }

    public void ShellSort(int n)								// DEBUGGED
    {
        #region COMMENT
        // Sorts the array in ascending order by Shell’s method (diminishing
        // increment sort). n is automatically set to the size of the array, unless you
        // specify another n, in which case only the first n elements of the array are
        // sorted.
        #endregion
        if (n <= 0 || Num < n) n = Num; if (n < 2) return;

        int i, j, inc;
        T v;
        inc = 1;							// Determine the starting increment.

        do { inc *= 3; inc++; } while (inc.CompareTo(n) < 0);

        do									// Loop over the partial sorts.
        {
            inc /= 3;

            for (i = inc; i < n; i++)		// Outer loop of straight insertion.
            {
                v = array[i];
                j = i;

                while (array[j - inc].CompareTo(v) > 0)	// Inner loop of straight insertion.
                {
                    array[j] = array[j - inc];
                    j -= inc;
                    if (j < inc) break;
                }

                array[j] = v;
            }

        } while (inc > 0);
    }

    public void QuickSort(int lo, int hi)						// DEBUGGED
    {
        #region COMMENT
        // Quick sort
        // lo is the lower index, hi is the upper index of the region of
        // the array that is to be sorted.
        #endregion

        if (hi == -1) hi = Num - 1;

        int i = lo, j = hi;
        T h;
        T x = array[(lo + hi) / 2];

        //  partition
        do
        {
            while (array[i].CompareTo(x) < 0) i++;
            while (array[j].CompareTo(x) > 0) j--;
            if (i <= j)
            {
                h = array[i]; array[i] = array[j]; array[j] = h;
                i++; j--;
            }
        } while (i <= j);

        //  recursion
        if (lo < j) QuickSort(lo, j);
        if (i < hi) QuickSort(i, hi);
    }

    #endregion	// SORT

    #region SET OPERATIONS

    public Stats<T> Union(Stats<T> a)						// DEBUGGED
    {
        Stats<T> temp = new Stats<T>();

        for (int i = 0; i < Num; i++)
        {
            if (temp.Has(array[i])) temp.Add(array[i]);
        }

        for (int j = 0; j < a.Num; j++)
        {
            if (temp.Has(a.array[j])) temp.Add(a.array[j]);
        }

        temp.buffer = buffer;
        if (temp.Max - temp.Num >= temp.buffer) temp.Reduce();

        return temp;
    }
    // Returns an array of elements combined from both this array and 'a'.
    // Any such elements are added to the union array only once, so that no
    // two elements in the union are equal.

    public Stats<T> Intersection(Stats<T> a)						// DEBUGGED
    {

        Stats<T> temp = new Stats<T>();

        for (int i = 0; i < Num; i++)
        {
            if (temp.Has(array[i]))
            {
                if (a.Has(array[i])) temp.Add(array[i]);
            }
        }

        temp.buffer = buffer;
        if (temp.Max - temp.Num >= temp.buffer) temp.Reduce();

        return temp;
    }
    // Returns an array of elements found both in this array and in 'a'.
    // Any such elements are added to the union array only once, so that no
    // two elements in the intersection are equal.

    public Stats<T> Difference(Stats<T> a)						// DEBUGGED
    {
        Stats<T> temp = new Stats<T>();

        for (int i = 0; i < Num; i++)
        {
            if (temp.Has(array[i]))
            {
                if (a.Has(array[i])) temp.Add(array[i]);
            }
        }

        temp.buffer = buffer;
        if (temp.Max - temp.Num >= temp.buffer) temp.Reduce();

        return temp;
    }
    // Returns an array of elements found in this array but not in 'a'.
    // Any such elements are added to the union array only once, so that no
    // two elements in the difference are equal.

    #endregion	// SETOPS

    public override string ToString()
    {
        char[] s = null;
        string str = new string(s);

        for (int i = 0; i < Num; i++) str += array[i].ToString();
        return str;
    }
}


/// <summary>
/// A set is a collection of unique elements, such that no two elements may have the same value.
/// <para>Two sets are equal if every value of one is also a member of the second, disregarding order.</para>
/// </summary>
/// <typeparam name="T">Data type of the elements of the set.</typeparam>
[Serializable]
public class Record<T> : IEnumerator<T>, IEnumerable<T>, ISerializable
{
    #region DATA
    /// <summary>Buffer size.</summary>
    protected int buffer;
    /// <summary>Array of elements.</summary>
    protected Entry[] array;
    /// <summary>Number of allocated elements.</summary>
    protected int Max;
    /// <summary>Number of used elements.</summary>
    protected int Num;
    /// <summary>Default element value.</summary>
    protected T dval = default(T);
    /// <summary>Random number generator.</summary>
    protected Random random = new Random();

    protected int enumerationPosition = -1;
    #endregion

    #region TYPES
    protected class Entry
    {
        public T        Member;
        public object   Record;

        public static implicit operator T ( Entry entry )
        {
            return entry.Member;
        }
    }
    #endregion

    #region CONSTRUCTION (Tested)
    /// <summary>
    /// Default constructor with initial size 30 and buffer count 30.
    /// </summary>
    public Record   ( )
    {
        array = null; buffer = 30;
        Num = 0; Max = 30;

        InitArray();
    }
    /// <summary>
    /// Constructor for initial capacity. Buffer count 30.
    /// </summary>
    /// <param name="max">Initial array capacity.</param>
    public Record   ( int max )
    {
        array = null; buffer = 30;
        Num = 0; Max = max;

        InitArray();
    }
    /// <summary>
    /// Copy the items of the array, excluding duplicate values.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Source array.</param>
    public Record   ( T[] a )
    {
        buffer = 30;
        Max = (a == null) ? 0 : a.Length; Num = 0;

        InitArray();
        for (int i = 0; i < Max; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if ( a[i].Equals(array[j].Member ) )
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else array[Num++] = new Entry() { Member = a[i], Record = this };
        }
    }
    /// <summary>
    /// Copy constructor.
    /// </summary>
    /// <param name="a">Source list.</param>
    public Record   ( Record<T> a )
    {
        buffer = 30;
        Num = (a == null) ? 0 : a.Length; Max = Num;

        InitArray();
        for (int i = 0; i < Num; i++) array[i] = new Entry() { Member = a.array[i], Record = this };
    }
    /// <summary>
    /// Destructor.
    /// </summary>
    ~Record()
    {
        Delete();
    }
    #endregion		// CONSTRUCTION

    #region MEMORY OPERATIONS (Tested)
    /// <summary>
    /// Allocate memory buffer.
    /// </summary>
    protected void InitArray()
    {
        array = new Entry[Max];
    }
    /// <summary>
    /// Deallocate memory.
    /// </summary>
    protected void Delete()
    {
        array = null;
        Num = 0;
        Max = 0;
    }
    /// <summary>
    /// Empty the array.
    /// </summary>
    public void    Clear()
    {
        Delete(); Num = 0; Max = buffer;
        InitArray();
    }
    /// <summary>
    /// Expand and reallocate.
    /// </summary>
    /// <param name="size">Expansion count.</param>
    protected void Expand(int size)
    {
        if (size < 1) return;
        Entry[] temp = new Entry[Max + size];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max += size;
    }
    /// <summary>
    /// Free unused memory.
    /// </summary>
    protected void Reduce()
    {
        Entry[] temp = new Entry[Num];

        for (int i = 0; i < Num; i++) temp[i] = array[i];

        array = temp;
        Max = Num;
    }
    #endregion

    #region PROPERTIES (Tested)
    /// <summary>
    /// Get the item at an index, or set the item at an index if its not already a member.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="i">Index of an item.</param>
    /// <returns>The item at the index.</returns>
    public virtual T this[int i]
    {
        get
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();
            return array[i].Member;
        }
        set
        {
            if (i < 0 || Num <= i) throw new IndexOutOfRangeException();

            for (int j = 0; j < Num; j++)
            {
                if (array[j].Equals(value)) return;
            }

            array[i] = new Entry() { Member = value, Record = this };
        }
    }
    /// <summary>Get or set the memory buffer.</summary>
    public int Buffer { set { buffer = value; } get { return buffer; } }
    /// <summary>Get a copy of the internal System.Array.</summary>
    public T[] Array
    {
        get
        {
            T[] clone = new T[Num];
            for (int i = 0; i < Num; i++) clone[i] = array[i].Member;
            return clone;
        }
    }

    /// <summary>Get the number of items added to the collection.</summary>
    public int Length { get { return Num; } }
    /// <summary>Get the size of the collection including empty elements.</summary>
    public int Size { get { return Max; } }
    public T DefaultValue { set { dval = value; } get { return dval; } }
    #endregion

    #region OPERATORS (Tested)
    /// <summary>
    /// Equality operator. Test if two sets have identical contents,
    /// disregarding order.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if every member of one set is also a member of the other set.</returns>
    public static bool operator ==		( Record<T> a, Record<T> b )
    {
        if (object.ReferenceEquals(a, b)) return true;
        else if (object.ReferenceEquals(a, null)) return false;
        else if (object.ReferenceEquals(b, null)) return false;
        else if (a.Num != b.Num) return false;

        for ( int i = 0 ; i < a.Num ; i++ )
        {
            bool exists = false;
            for ( int j = 0; j < b.Num; j++ )
            {
                if ( a.array[i].Member.Equals(b.array[j].Member ) )
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) return false;
        }

        return true;
    }
    /// <summary>
    /// Inequality operator. Test if two sets are not identical.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to compare.</param>
    /// <param name="b">Set to compare to.</param>
    /// <returns>True if any value is in one set but not the other.</returns>
    public static bool operator !=		( Record<T> a, Record<T> b )
    {
        if (ReferenceEquals(a, b)) return false;
        else if (ReferenceEquals(a, null)) return true;
        else if (ReferenceEquals(b, null)) return true;
        else if (a.Num != b.Num) return true;

        for (int i = 0; i < a.Num; i++)
        {
            bool exists = false;
            for (int j = 0; j < b.Num; j++)
            {
                if ( a.array[i].Member.Equals( b.array[j].Member ) )
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) return true;
        }

        return false;
    }
    /// <summary>
    /// Union operator. Appends one item to the end of the set if its
    /// not already a member of the set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to append to.</param>
    /// <param name="b">Item to append.</param>
    /// <returns>The union of the set and the item.</returns>
    public static Record<T> operator +	( Record<T> a, T b )
    {
        Record<T> union = new Record<T>(a);
        union.Add(b);
        return union;
    }
    /// <summary>
    /// Union operator. Appends any element of the second operand to the end
    /// of the first, if its not already a member of that set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">First source set.</param>
    /// <param name="b">Set to append.</param>
    /// <returns>The union of the two sets.</returns>
    public static Record<T> operator +	( Record<T> a, Record<T> b )
    {
        Record<T> union = new Record<T>(a);
        union.Add( b );
        return union;
    }
    /// <summary>
    /// Set difference operator.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to reduce.</param>
    /// <param name="b">Element to remove.</param>
    /// <returns>A set of all elements of a that are not equal to b.</returns>
    public static Record<T> operator -	( Record<T> a, T b )
    {
        Record<T> difference = new Record<T>( a.Num );
        int num = 0;

        for (int i = 0; i < a.Num; i++)
        {
            if ( a.array[i].Member.Equals( b ) ) continue;
            else difference.array[num++] = a.array[i];
        }

        difference.Num = num;
        return difference;
    }
    /// <summary>
    /// Set difference operator.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="a">Set to reduce.</param>
    /// <param name="b">Set to remove.</param>
    /// <returns>All elements of a that are not in b.</returns>
    public static Record<T> operator -	( Record<T> a, Record<T> b )
    {
        Record<T> difference = new Record<T>(a.Num);
        int num = 0;

        for (int i = 0; i < a.Length; i++)
        {
            bool match = false;
            for (int j = 0; j < b.Num; j++)
            {
                if (a.array[i].Member.Equals(b.array[j]))
                {
                    match = true;
                    break;
                }
            }

            if (match) continue;
            else difference.array[num++] = a.array[i];
        }

        difference.Num = num;
        return difference;
    }
    #endregion	// OPERATORS

    #region SEARCH
    /// <summary>
    /// Determine if a value is in the list.
    /// </summary>
    /// <param name="e">Search value.</param>
    /// <returns>True if found. False otherwise.</returns>
    public bool			Has			( T e )
    {
        for (int i = 0; i < Num; i++)
        {
            if ( array[i].Member.Equals( e ) ) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if a given array is a subarray of this array.
    /// </summary>
    /// <param name="a">Array to check.</param>
    /// <returns>True if all items of a appear in an unbroken sequence in this array.</returns>
    public bool			Has			( Record<T> a )
    {
        for (int i = 0; i <= Num - a.Num; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Num; j++)
            {
                if ( !a.array[j].Member.Equals( array[i + j].Member ) )
                {
                    match = false;
                    break;
                }
            }
            if ( match ) return true;
        }

        return false;
    }
    /// <summary>
    /// Check if any one item of a set appears in this array.
    /// </summary>
    /// <param name="record">Items to find.</param>
    /// <returns>True if at least one item appear this array.</returns>
    public bool         HasAny      ( params T[] record )
    {
        for (int i = 0; i < record.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (record[i].Equals(array[j].Member)) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if the set of items of an array intersects with the items of this array, disregarding order.
    /// </summary>
    /// <param name="record">Set of items.</param>
    /// <returns>True if at least one item is a member of both arrays.</returns>
    public bool         HasAny      ( Record<T> record )
    {
        for (int i = 0; i < record.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (record.array[i].Member.Equals(array[j].Member)) return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// </summary>
    /// <param name="record">Items to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool         HasAll      ( params T[] record )
    {
        for (int i = 0; i < record.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (record[i].Equals(array[j].Member))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Check if a set of items is a subset of the items in the array, disregarding order.
    /// </summary>
    /// <param name="record">Item to find.</param>
    /// <returns>True if every item of the set appear in this array.</returns>
    public bool         HasAll      ( Record<T> record )
    {
        for (int i = 0; i < record.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if (record.array[i].Member.Equals(array[j].Member))
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue;
            else return false;
        }

        return true;
    }
    /// <summary>
    /// Find the index of a member.
    /// </summary>
    /// <param name="e">Searched item.</param>
    /// <returns>An index or -1 if the item is not in the array.</returns>
    public int          Find        ( T e )
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Member.Equals(e)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the index of a member starting the search at index b.
    /// </summary>
    /// <param name="b">Start index of the search.</param>
    /// <param name="e">Item to find.</param>
    /// <returns>True if the item was found (its index output to b), false otherwise.</returns>
    public bool         FindNext    ( ref int b, T e )
    {
        for (int i = b; i < Num; i++)
        {
            if (array[i].Member.Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the start index of a subarray starting the search at index b.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="a">Array to find.</param>
    /// <returns>True if a subarray was found (its index output to b), false otherwise.</returns>
    public bool         FindNext    ( ref int b, Record<T> a )
    {
        for (int i = b; i <= Num - a.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < a.Length; j++)
            {
                if (!a.array[j].Member.Equals(array[i + j].Member))
                {
                    match = false;
                    break;
                }
            }
            if (match) { b = i; return true; }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the index of a member, starting the search at b and going backwards (towards lower
    /// indeces).
    /// </summary>
    /// <param name="b">Start index of search</param>
    /// <param name="e">Searched item.</param>
    /// <returns>True if the item was found, false otherwise. Its index or -1 is
    /// stored in the ref parameter.</returns>
    public bool         FindPrior   ( ref int b, T e )
    {
        for (int i = b; i >= 0; i--)
        {
            if (array[i].Member.Equals(e))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the start index of a sequence ending before or at the 
    /// given index, searching towards lower indexes.
    /// </summary>
    /// <param name="b">The search starts at b+1-log.Length.</param>
    /// <param name="record">Sequence of items to find.</param>
    /// <returns>True if the sequence was found, false otherwise. Its start index
    /// is stored in the ref parameter.</returns>
    public bool         FindPrior   ( ref int b, Record<T> record )
    {
        for (int i = b + 1 - record.Num; i >= 0; i--)
        {
            bool match = true;
            for (int j = 0; j < record.Num; j++)
            {
                if (!record.array[j].Member.Equals(array[i + j].Member))
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the first element from the given index that does not match any of
    /// the given values, but is preceded by one such.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="e">Values find and skip past.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool         FindPast    ( ref int b, params T[] e )
    {
        int ie = -1; // index of the last element matching any of the parameter values.

        for (int i = b; i < Num; i++)
        {
            for (int j = 0; j < e.Length; j++)
            {
                if (array[i].Member.Equals(e[j]))
                {
                    ie = i;
                    break;
                }
                else if (ie != -1)
                {
                    b = i;
                    return true;
                }
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Get the intersection of an array with this array.
    /// </summary>
    /// <param name="record">A set of items.</param>
    /// <returns>An array of all items of the given array that are also members of this array.</returns>
    public Record<T>    Intersection( Record<T> record )
    {
        Record<T> intersection = new Record<T>();

        for (int i = 0; i < record.Length; i++)
        {
            for (int j = 0; j < Num; j++)
            {
                if (record.array[i].Member.Equals(array[j].Member))
                {
                    intersection.Add(record.array[i]);
                    break;
                }
            }
        }

        return intersection;
    }

    /// <summary>
    /// Determine if the collection has a member that matches the condition of the
    /// given delegate.
    /// </summary>
    /// <param name="condition">Delegate function.</param>
    /// <returns>True if a match is found, false otherwise.</returns>
    public bool         Has         ( Func<T, bool> condition )
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i].Member)) return true;
        }

        return false;
    }
    /// <summary>
    /// Find the index of the first item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the match or -1.</returns>
    public int          FindFirst   ( Func<T, bool> condition )
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i].Member)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the index of the last item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item as a parameter and returning true or false.</param>
    /// <returns>The index of the matching item or -1 if no match is found.</returns>
    public int          FindLast    ( Func<T, bool> condition )
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i].Member)) return i;
        }
        return -1;
    }
    /// <summary>
    /// Find the first item matching the condition of the given delegate, starting
    /// the search from the given index.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function.</param>
    /// <returns>True if a match is found, false otherwise. The index or -1 is
    /// stored in the ref parameter.</returns>
    public bool         FindNext    ( ref int b, Func<T, bool> condition )
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i].Member))
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Find the last item before or at the given index matching the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>True if a match was found, otherwise false. Its index or -1 is stored in the
    /// ref parameter.</returns>
    public bool         FindPrior   ( ref int b, Func<T, bool> condition )
    {
        for (int i = b; i >= 0; i--)
        {
            if (condition(array[i].Member))
            {
                b = i;
                return true;
            }
        }
        b = -1;
        return false;
    }
    /// <summary>
    /// Find the first element from the given index that does not match the
    /// condition of the given delegate, but is preceded by such a match.
    /// </summary>
    /// <param name="b">Beginning index of search.</param>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>True if a value is found. Its index or -1 is stored in the ref parameter.</returns>
    public bool         FindPast    ( ref int b, Func<T, bool> condition )
    {
        int m = -1; // index of the last element matching the condition.

        for (int i = b; i < Num; i++)
        {
            if (condition(array[i].Member))
            {
                m = i;
            }
            else if (m != -1)
            {
                b = i;
                return true;
            }
        }

        b = -1;
        return false;
    }
    /// <summary>
    /// Get the first item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A matching item or the default value of the type if no match is found.
    /// The default value is null for reference types and zero for value types.</returns>
    public T            GetFirst    ( Func<T, bool> condition )
    {
        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i].Member)) return array[i].Member;
        }
        return default(T);
    }
    /// <summary>
    /// Get the last item matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>An item or the default value of the type if no match is found (null for
    /// reference types, zero for value types).</returns>
    public T            GetLast     ( Func<T, bool> condition )
    {
        for (int i = Num - 1; i >= 0; i--)
        {
            if (condition(array[i].Member)) return array[i].Member;
        }
        return default(T);
    }
    /// <summary>
    /// Get the first item from the given index that matches the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Index to begin search at.</param>
    /// <param name="condition">Delegate taking an item and returning a bool.</param>
    /// <returns>An item if found or the default value of the type (zero for value types,
    /// null for reference types). Its index or -1 is stored in the ref parameter.</returns>
    public T            GetNext     ( ref int b, Func<T, bool> condition )
    {
        for (int i = b; i < Num; i++)
        {
            if (condition(array[i].Member)) { b = i; return array[i].Member; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get the last item before or at the given index matching the condition
    /// of the given delegate.
    /// </summary>
    /// <param name="b">Start index of search.</param>
    /// <param name="condition">Delegate function taking an item as parameter and returning a bool.</param>
    /// <returns>An item or the default value of the type (zero for value types, null for reference types.
    /// Its index or -1 is stored in the ref parameter.</returns>
    public T            GetPrior    ( ref int b, Func<T, bool> condition )
    {
        for (int i = b; i >= 0; i--)
        {
            if (condition(array[i].Member)) { b = i; return array[i].Member; }
        }

        b = -1;
        return default(T);
    }
    /// <summary>
    /// Get a collection of all items matching the condition of the given delegate.
    /// </summary>
    /// <param name="condition">Delegate function taking an item and returning a bool.</param>
    /// <returns>A collection of items.</returns>
    public Record<T>    GetAll      ( Func<T, bool> condition )
    {
        Record<T> record = new Record<T>();

        for (int i = 0; i < Num; i++)
        {
            if (condition(array[i].Member)) record.Add(array[i].Member);
        }
        return record;
    }
    #endregion SEARCH

    #region EDIT
    /// <summary>
    /// Append an item to the set if its not already a member of the set.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="e">Appending item.</param>
    /// <returns>True if appended, false otherwise.</returns>
    public bool         Add         ( T e )
    {
        for (int i = 0; i < Num; i++)
        {
            if (array[i].Member.Equals(e)) return false;
        }

        if (Num + 1 > Max) Expand(1 + Buffer);

        array[Num++] = new Entry() { Member = e, Record = this };
        return true;
    }
    /// <summary>
    /// Append all elements of a set to this set, excluding elements
    /// that are already members of this set. This set becomes the
    /// union of the two sets.
    /// <para>Tested.</para>
    /// </summary>
    /// <param name="s">Appending set.</param>
    /// <returns>True if all members were added, false otherwise.</returns>
    public bool         Add         ( Record<T> s )
    {
        bool itemexcluded = false;

        // Iterate through the appending items:
        for (int i = 0; i < s.Length; i++)
        {
            // Check if the appending item is already in this set.
            bool exists = false;
            for (int j = 0; j < Num; j++)
            {
                if ( s.array[i].Member.Equals(array[j].Member ) )
                {
                    exists = true;
                    break;
                }
            }

            // Add the appending item if it was not in this set.
            if (exists)
            {
                itemexcluded = true;
                continue;
            }
            else
            {
                if (Num + 1 > Max) Expand(1 + Buffer);
                array[Num++] = new Entry() { Member = s[i], Record = this };
            }
        }

        return !itemexcluded;
    }
    /// <summary>
    /// Remove an item at a given index.
    /// </summary>
    /// <param name="index">Index of item to remove.</param>
    /// <returns>The removed item.</returns>
    public T            RemoveAt    ( int index )
    {
        T temp = array[index].Member;

        // Left shift all items after index.
        for (int i = index; i < Num - 1; i++)
        {
            array[i] = array[i + 1];
        }

        Num--;
        if (Max - Num > Buffer) Reduce();

        return temp;
    }
    /// <summary>
    /// Remove the item of the given value from the array.
    /// </summary>
    /// <param name="e">Value to remove.</param>
    /// <returns>True if the item was found, false otherwise.</returns>
    public bool         Remove      ( T e )
    {
        int i = 0, s = 0;

        while (i < Num)
        {
            if ( array[i].Member.Equals( e ) ) s++;
            else array[i - s] = array[i];
            i++;
        }

        Num -= s;

        if (Max - Num > Buffer) Reduce();

        if (s == 0) return false;
        else return true;
    }
	/// <summary>
	/// Swap a member with the member of the next lower index.
	/// </summary>
	/// <param name="e">Member to left-shift.</param>
	public void         ShiftLeft   ( T e )
	{
		int i = Find( e );
		if ( i > 0 )
		{
			Entry temp = array[i-1];
			array[i-1] = array[i];
			array[i] = temp;
		}
	}
	/// <summary>
	/// Swap a member with the member of the next higher index.
	/// </summary>
	/// <param name="e">Member to right-shift.</param>
	public void         ShiftRight  ( T e )
	{
		int i = Find( e );
		if ( i < Num - 1 )
		{
			Entry temp = array[i+1];
			array[i+1] = array[i];
			array[i] = temp;
		}
	}
	/// <summary>
    /// Exchange the positions of two elements identified by index.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
    public void         Swap        ( int i, int j )
    {
        if (i == j) return;

        Entry temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    /// <summary>
    /// Exchange the positions of two elements.
    /// </summary>
    /// <param name="i">Index of an element.</param>
    /// <param name="j">Index of an element.</param>
	public void Swap( T a, T b )
	{
		int ia = -1, ib = -1;

		for ( int i = 0 ; i < Num ; i++ )
		{
			if		( array[i].Member.Equals( a ) ) ia = i;
			else if ( array[i].Member.Equals( b ) ) ib = i;

			if ( ia != -1 && ib != -1 )
			{
				Entry temp = array[ia];
				array[ia] = array[ib];
				array[ib] = temp;
				return;
			}
		}
	}
	#endregion // EDIT

    #region INTERFACES
    /// <summary>
    /// Two sets are equal if they have the same number of elements and
    /// all elements exist in both sets, disregarding order.
    /// </summary>
    /// <param name="o">Comparison list.</param>
    /// <returns>True if item count and all items are equal. False otherwise.</returns>
    public override bool    Equals      ( object o )
    {
        if (o is Record<T>) return this == (Record<T>)o;
        else return false;
    }
    public override int     GetHashCode ( )
    {
        return array.GetHashCode();
    }
    public IEnumerator<T> GetEnumerator()
    {
        enumerationPosition = -1;
        return (IEnumerator<T>)this;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public bool MoveNext()
    {
        if (++enumerationPosition >= Length) { Reset(); return false; }
        else return true;
    }
    public void Reset()
    {
        enumerationPosition = -1;
    }
    void IDisposable.Dispose() { }
    public T Current
    {
        get { return array[enumerationPosition].Member; }
    }
    object System.Collections.IEnumerator.Current
    {
        get { return Current; }
    }
    #endregion
    #region SERIALIZATION
    protected Record(SerializationInfo s, StreamingContext c)
    {
        this.buffer = s.GetInt32("buffer");
        this.Max = s.GetInt32("max");
        this.Num = s.GetInt32("num");
        this.dval = (T)s.GetValue("dval", typeof(T));

        this.array = (Entry[])s.GetValue("array", typeof(Entry[]));
    }
    protected virtual void GetObjectData(SerializationInfo s, StreamingContext c)
    {
        s.AddValue("buffer", buffer);
        s.AddValue("max", Max);
        s.AddValue("num", Num);
        s.AddValue("dval", dval);

        s.AddValue("array", array);
    }
    void ISerializable.GetObjectData(SerializationInfo s, StreamingContext c)
    {
        this.GetObjectData(s, c);
    }
    #endregion
}


//___________________________________________________________________________________________
//
//	EXCEPTION HANDLING
//___________________________________________________________________________________________
//

#region EXCEPTIONS

class ERROR_Field : Exception			// DEBUGGED
{

    private string message;

    public ERROR_Field(string message) { this.message = message; }
    public string ErrorMessage() { return message; }
}

#endregion	// EXCEPTIONS

