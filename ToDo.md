🧩 1️⃣ you can apply it globally

instead of decorating each enum individually, you can make all enums in your app serialize as strings:

builder.Services.AddControllers()
.AddJsonOptions(opt =>
{
opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

→ now every enum will serialize to/from strings automatically.

🧩 2️⃣ you can control casing (optional)

by default, the converter outputs enum names exactly as declared (e.g., "Basic", "Advance").
but you can also force camelCase or other naming with a JsonNamingPolicy:

new JsonStringEnumConverter(JsonNamingPolicy.CamelCase);

then "Basic" → "basic", "Advance" → "advance".

🧩 3️⃣ and it works both ways

deserialization also respects it:

{ "membershipTier": "High" }

→ automatically becomes MembershipTier.High.

so no custom converters or manual parsing needed
