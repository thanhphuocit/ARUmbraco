//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.13.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Structure.Model.DocumentTypes
{
	/// <summary>Site Control</summary>
	[PublishedModel("siteControl")]
	public partial class SiteControl : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		public new const string ModelTypeAlias = "siteControl";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<SiteControl, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public SiteControl(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Description
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		[ImplementPropertyType("description")]
		public virtual string Description => this.Value<string>("description");

		///<summary>
		/// LogoTop
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		[ImplementPropertyType("logoTop")]
		public virtual global::Umbraco.Core.Models.PublishedContent.IPublishedContent LogoTop => this.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("logoTop");

		///<summary>
		/// SiteName
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		[ImplementPropertyType("siteName")]
		public virtual string SiteName => this.Value<string>("siteName");

		///<summary>
		/// Small Icon Site
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.13.0")]
		[ImplementPropertyType("smallIconSite")]
		public virtual global::Umbraco.Core.Models.PublishedContent.IPublishedContent SmallIconSite => this.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("smallIconSite");
	}
}
