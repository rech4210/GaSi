
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
struct Attribute
{
    float3 positionOS: POSITION;
    float2 uv : TEXCOORD0;
};

struct Varyings
{
    float4 posCS: SV_POSITION; // Ŭ�� �����̽��� ���ؽ� ��ġ�� �����ϰ� �ִٴ� Syntax
    float2 uv : TEXCOORD0;
};

float4 _BaseColor;
TEXTURE2D(_MainTex); // TEXTURE2D �Լ��� 
SAMPLER(sampler_MainTex); // �ؽ��ĸ� �ҷ������� ���� ��ũ�� ������ ���÷� �������� �ؽ���
float4 _MainTex_ST; // ST Ÿ�Կ� ����Ƽ�� �ڵ������� ���� ��������

#define MACRO(argument) argument + 2 // ��ũ��

Varyings vert(Attribute input)
{
    Varyings output;
    VertexPositionInputs posinput = GetVertexPositionInputs(input.positionOS)
    ;
    // GetVertexPositionInputs �� ���ؽ��� ��Ʈ���� ������ �ϴ� �κ���.
    output.posCS = posinput.positionCS;
    output.uv = TRANSFORM_TEX(input.uv, _MainTex); // �ش� �Լ� �κп��� UV�� �ٲ���
    return output;
}

//#define TRANSFORM_TEX(tex, name) (tex.xy * name##_ST.xy,name##_ST.zw)
// TRANSFORM_TEX���� ���� �ؽ�Ʈ�� _ST�� �ٲ� ������ ��������

float4 frag(Varyings i) : SV_TARGET // ���� �ȼ����� �����ϴ� �Լ���� �ǹ�
{
    float4 colorSample = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
    

    InputData lightingInput = (InputData)0;
    SurfaceData surfaceInput = (SurfaceData)0;

    return UniversalFragmentBlinnPhong(lightingInput,surfaceInput);
}
